namespace Eurekabank_Cliente_Dotnet_Rest_G6.Models
{
    public class Cuenta
    {
        public string chr_cuencodigo { get; set; } // Código de cuenta
        public decimal dec_cuensaldo { get; set; } // Saldo de cuenta
        public int int_cuencontmov { get; set; } // Contador de movimientos
        public string vch_cuenestado { get; set; } // Estado de cuenta
    }
}
