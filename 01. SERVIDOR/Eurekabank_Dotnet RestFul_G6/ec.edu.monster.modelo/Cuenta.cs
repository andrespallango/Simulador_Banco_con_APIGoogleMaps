using System.ComponentModel.DataAnnotations;

namespace Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.modelo
{
    public class Cuenta
    {
        [Key]
        public string chr_cuencodigo { get; set; } // chr_cuencodigo
        public decimal dec_cuensaldo { get; set; } // dec_cuensaldo
        public int int_cuencontmov { get; set; } // int_cuencontmov
        public string vch_cuenestado { get; set; } // vch_cuenestado
    }
}