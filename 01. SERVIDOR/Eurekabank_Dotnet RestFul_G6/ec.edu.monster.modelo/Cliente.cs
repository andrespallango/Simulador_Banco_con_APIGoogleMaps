using System.ComponentModel.DataAnnotations;

namespace Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.modelo
{
    public class Cliente
    {
        [Key]
        public string CliCodigo { get; set; } // chr_cliecodigo
        public string CliCedula { get; set; } // chr_cliedni
        public string CliNombre { get; set; } // vch_clienombre
        public string CliApellido { get; set; } // vch_cliepaterno
        public string CliCorreo { get; set; } // vch_clieemail
        public string CliTelefono { get; set; } // vch_clietelefono
    }
}
