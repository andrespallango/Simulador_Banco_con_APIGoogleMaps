using System.ComponentModel.DataAnnotations;

namespace Eurekabank_Cliente_Dotnet_Rest_G6.Models
{
    public class Cliente
    {
        [Key]
        public int CliId { get; set; }

        public string CliCedula { get; set; }

        public string CliNombre { get; set; }

        public string CliApellido { get; set; }

        public string CliCorreo { get; set; }

        public string CliTelefono { get; set; }
    }
}
