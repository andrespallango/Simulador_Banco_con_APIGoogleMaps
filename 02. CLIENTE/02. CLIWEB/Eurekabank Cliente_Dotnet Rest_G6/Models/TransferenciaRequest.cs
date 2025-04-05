namespace Eurekabank_Cliente_Dotnet_Rest_G6.Models
{
    public class TransferenciaRequest
    {
        public string CuentaOrigen { get; set; }
        public string CuentaDestino { get; set; }
        public decimal Valor { get; set; }
    }
}
