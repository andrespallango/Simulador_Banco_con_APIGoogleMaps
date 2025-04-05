namespace Eurekabank_Cliente_Dotnet_Rest_G6.Models
{
    public class TipoMovimiento
    {
        public string chr_tipocodigo { get; set; } // Código del tipo de movimiento
        public string vch_tipodescripcion { get; set; } // Descripción del tipo de movimiento
        public string vch_tipoaccion { get; set; } // Acción del tipo de movimiento (ej. Débito, Crédito)
        public string vch_tipoestado { get; set; } // Estado del tipo de movimiento (ej. Activo, Inactivo)
    }
}
