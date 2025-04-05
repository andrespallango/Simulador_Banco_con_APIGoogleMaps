using System;
using System.ComponentModel.DataAnnotations;

namespace Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.modelo
{
    public class Movimiento
    {
        [Key]
        public int int_movinumero { get; set; } // int_movinumero

        public string chr_cuencodigo { get; set; } // chr_cuencodigo
        public DateTime? dtt_movifecha { get; set; } // dtt_movifecha
        public string chr_emplcodigo { get; set; } // chr_emplcodigo
        public string chr_tipocodigo { get; set; } // chr_tipocodigo
        public decimal dec_moviimporte { get; set; } // dec_moviimporte
        public string chr_cuenreferencia { get; set; } // chr_cuenreferencia
    }
}