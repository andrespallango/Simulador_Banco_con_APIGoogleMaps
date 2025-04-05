using Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.modelo;

namespace Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.servicio
{
    public class UsuarioService
    {
        private readonly List<Usuario> usuarios;

        public UsuarioService()
        {
            usuarios = new List<Usuario>
            {
                new Usuario { Username = "usuario1", PasswordHash = "contrasena1" },
                new Usuario { Username = "usuario2", PasswordHash = "contrasena2" },
                new Usuario { Username = "monster", PasswordHash = "monster9" }
            };
        }

        public Usuario Autenticar(string username, string password)
        {
            // Log para verificar los datos recibidos
            Console.WriteLine($"Autenticando usuario: {username}");

            return usuarios.FirstOrDefault(u => u.Username == username && u.PasswordHash == password);
        }
    }
}
