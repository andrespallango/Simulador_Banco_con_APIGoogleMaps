using System;

namespace Eurekabank_Cliente_Dotnet_Rest_G6.Models
{
    public class Movimiento
    {
        public int int_movinumero { get; set; } // Número del movimiento
        public string chr_cuencodigo { get; set; } // Código de la cuenta
        public DateTime? dtt_movifecha { get; set; } // Fecha del movimiento
        public string chr_tipocodigo { get; set; } // Ahora contiene la descripción del tipo de movimiento
        public decimal dec_moviimporte { get; set; } // Monto del movimiento
        public string chr_cuenreferencia { get; set; } // Referencia de la cuenta
    }
}
