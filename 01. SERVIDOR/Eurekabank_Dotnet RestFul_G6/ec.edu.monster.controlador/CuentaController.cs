using Microsoft.AspNetCore.Mvc;
using Eurekabank_Dotnet_RestFul_G6.Services;
using Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.modelo;
using System.Collections.Generic;

namespace Eurekabank_Dotnet_RestFul_G6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaController : ControllerBase
    {
        private readonly CuentaService _cuentaService;

        public CuentaController(CuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        [HttpGet("{cuentaCodigo}/movimientos")]
        public ActionResult<List<Movimiento>> ObtenerMovimientosPorCuenta(string cuentaCodigo)
        {
            var movimientos = _cuentaService.LeerMovimientos(cuentaCodigo);
            return Ok(movimientos);
        }

        [HttpPost("{cuentaCodigo}/deposito")]
        public IActionResult RegistrarDepositos(string cuentaCodigo, [FromBody] double importe)
        {
            _cuentaService.RegistrarDepositos(cuentaCodigo, importe, "0001");
            return Ok();
        }

        [HttpPost("{cuentaCodigo}/retiro")]
        public IActionResult RegistrarRetiros(string cuentaCodigo, [FromBody] double importe)
        {
            _cuentaService.RegistrarRetiros(cuentaCodigo, importe, "0001");
            return Ok();
        }

        [HttpPost("transferencia")]
        public IActionResult RegistrarTransferencia([FromBody] TransferenciaRequest transferenciaRequest)
        {
            _cuentaService.RegistrarTransferencia(transferenciaRequest.CuentaOrigenCodigo, transferenciaRequest.CuentaDestinoCodigo, transferenciaRequest.Importe, "0001");
            return Ok();
        }
    }

    public class TransferenciaRequest
    {
        public string CuentaOrigenCodigo { get; set; }
        public string CuentaDestinoCodigo { get; set; }
        public double Importe { get; set; }
    }
}