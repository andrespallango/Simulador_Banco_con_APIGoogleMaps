using Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.modelo;
using Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.servicio;
using Microsoft.AspNetCore.Mvc;

namespace Eurekabank_Dotnet_RestFul_G6.ec.edu.monster.controlador
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly UsuarioService usuarioService;

        public LoginController()
        {
            usuarioService = new UsuarioService();
        }

        [HttpPost]
        public IActionResult Login([FromBody] Usuario loginRequest)
        {
            // Log para verificar los datos recibidos
            Console.WriteLine("Datos recibidos en el servidor:");
            Console.WriteLine($"Username: {loginRequest.Username}");
            Console.WriteLine($"Password: {loginRequest.PasswordHash}");

            // Autenticación
            var usuario = usuarioService.Autenticar(loginRequest.Username, loginRequest.PasswordHash);

            if (usuario != null)
            {
                Console.WriteLine("Autenticación exitosa.");
                return Ok(new { message = "Autenticación exitosa" });
            }
            else
            {
                Console.WriteLine("Autenticación fallida: Usuario o contraseña incorrectos.");
                return Unauthorized(new { message = "Usuario o contraseña incorrectos" });
            }
        }
    }
}
