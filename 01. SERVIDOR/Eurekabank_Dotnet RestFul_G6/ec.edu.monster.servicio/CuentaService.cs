using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.modelo;
using Eurekabank_Dotnet_RestFul_G6.Models;

namespace Eurekabank_Dotnet_RestFul_G6.Services
{
    public class CuentaService
    {
        private readonly EurekabankContext _context;

        public CuentaService(EurekabankContext context)
        {
            _context = context;
        }

        public List<Movimiento> LeerMovimientos(string cuentaCodigo)
        {
            var query = from m in _context.Movimientos
                        join t in _context.TipoMovimientos
                        on m.chr_tipocodigo equals t.chr_tipocodigo
                        where m.chr_cuencodigo == cuentaCodigo
                        orderby m.int_movinumero
                        select new Movimiento
                        {
                            chr_cuencodigo = m.chr_cuencodigo,
                            int_movinumero = m.int_movinumero,
                            dtt_movifecha = m.dtt_movifecha,
                            chr_tipocodigo = t.vch_tipodescripcion ?? string.Empty,
                            dec_moviimporte = m.dec_moviimporte
                        };

            return query.ToList();
        }
        public void RegistrarDepositos(string cuentaCodigo, double importe, string codEmp)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var cuenta = _context.Cuentas
                    .Where(c => c.chr_cuencodigo == cuentaCodigo && c.vch_cuenestado == "ACTIVO")
                    .FirstOrDefault();

                if (cuenta == null)
                {
                    throw new Exception("Error, cuenta no existe o no activa.");
                }

                cuenta.dec_cuensaldo += (decimal)importe;
                cuenta.int_cuencontmov++;
                _context.SaveChanges();

                // Bloquear la fila de la cuenta para evitar condiciones de carrera
                var nuevoNumeroMovimiento = _context.Movimientos
                    .Where(m => m.chr_cuencodigo == cuentaCodigo)
                    .OrderByDescending(m => m.int_movinumero)
                    .Select(m => m.int_movinumero)
                    .FirstOrDefault() + 1;

                var movimiento = new Movimiento
                {
                    int_movinumero = nuevoNumeroMovimiento,
                    chr_cuencodigo = cuentaCodigo,
                    dtt_movifecha = DateTime.Now,
                    chr_emplcodigo = codEmp,
                    chr_tipocodigo = "003",
                    dec_moviimporte = (decimal)importe,
                    chr_cuenreferencia = cuenta.dec_cuensaldo.ToString()
                };

                _context.Movimientos.Add(movimiento);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void RegistrarRetiros(string cuentaCodigo, double importe, string codEmp)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var cuenta = _context.Cuentas
                    .Where(c => c.chr_cuencodigo == cuentaCodigo && c.vch_cuenestado == "ACTIVO")
                    .FirstOrDefault();

                if (cuenta == null)
                {
                    throw new Exception("Error, cuenta no existe o no activa.");
                }

                if (cuenta.dec_cuensaldo < (decimal)importe)
                {
                    throw new Exception("Error, saldo insuficiente.");
                }

                cuenta.dec_cuensaldo -= (decimal)importe;
                cuenta.int_cuencontmov++;
                _context.SaveChanges();

                // Bloquear la fila de la cuenta para evitar condiciones de carrera
                var nuevoNumeroMovimiento = _context.Movimientos
                    .Where(m => m.chr_cuencodigo == cuentaCodigo)
                    .OrderByDescending(m => m.int_movinumero)
                    .Select(m => m.int_movinumero)
                    .FirstOrDefault() + 1;

                var movimiento = new Movimiento
                {
                    int_movinumero = nuevoNumeroMovimiento,
                    chr_cuencodigo = cuentaCodigo,
                    dtt_movifecha = DateTime.Now,
                    chr_emplcodigo = codEmp,
                    chr_tipocodigo = "004",
                    dec_moviimporte = (decimal)importe,
                    chr_cuenreferencia = cuenta.dec_cuensaldo.ToString()
                };

                _context.Movimientos.Add(movimiento);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public void RegistrarTransferencia(string cuentaOrigenCodigo, string cuentaDestinoCodigo, double importe, string codEmp)
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                var cuentaOrigen = _context.Cuentas
                    .Where(c => c.chr_cuencodigo == cuentaOrigenCodigo && c.vch_cuenestado == "ACTIVO")
                    .FirstOrDefault();

                if (cuentaOrigen == null)
                {
                    throw new Exception("Error, cuenta origen no existe o no activa.");
                }

                if (cuentaOrigen.dec_cuensaldo < (decimal)importe)
                {
                    throw new Exception("Error, saldo insuficiente en la cuenta origen.");
                }

                var cuentaDestino = _context.Cuentas
                    .Where(c => c.chr_cuencodigo == cuentaDestinoCodigo && c.vch_cuenestado == "ACTIVO")
                    .FirstOrDefault();

                if (cuentaDestino == null)
                {
                    throw new Exception("Error, cuenta destino no existe o no activa.");
                }

                cuentaOrigen.dec_cuensaldo -= (decimal)importe;
                cuentaDestino.dec_cuensaldo += (decimal)importe;
                cuentaOrigen.int_cuencontmov++;
                cuentaDestino.int_cuencontmov++;
                _context.SaveChanges();

                // Bloquear la fila de la cuenta para evitar condiciones de carrera
                var nuevoNumeroMovimientoOrigen = _context.Movimientos
                    .Where(m => m.chr_cuencodigo == cuentaOrigenCodigo)
                    .OrderByDescending(m => m.int_movinumero)
                    .Select(m => m.int_movinumero)
                    .FirstOrDefault() + 1;

                var nuevoNumeroMovimientoDestino = _context.Movimientos
                    .Where(m => m.chr_cuencodigo == cuentaDestinoCodigo)
                    .OrderByDescending(m => m.int_movinumero)
                    .Select(m => m.int_movinumero)
                    .FirstOrDefault() + 1;

                var movimientoOrigen = new Movimiento
                {
                    int_movinumero = nuevoNumeroMovimientoOrigen,
                    chr_cuencodigo = cuentaOrigenCodigo,
                    dtt_movifecha = DateTime.Now,
                    chr_emplcodigo = codEmp,
                    chr_tipocodigo = "009",
                    dec_moviimporte = (decimal)importe,
                    chr_cuenreferencia = cuentaOrigen.dec_cuensaldo.ToString()
                };

                var movimientoDestino = new Movimiento
                {
                    int_movinumero = nuevoNumeroMovimientoDestino,
                    chr_cuencodigo = cuentaDestinoCodigo,
                    dtt_movifecha = DateTime.Now,
                    chr_emplcodigo = codEmp,
                    chr_tipocodigo = "008",
                    dec_moviimporte = (decimal)importe,
                    chr_cuenreferencia = cuentaDestino.dec_cuensaldo.ToString()
                };

                _context.Movimientos.Add(movimientoOrigen);
                _context.Movimientos.Add(movimientoDestino);
                _context.SaveChanges();

                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}