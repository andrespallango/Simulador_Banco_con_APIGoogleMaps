using Eurekabank_Cliente_Dotnet_Rest_G6.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Eurekabank_Cliente_Dotnet_Rest_G6.Controllers
{
    public class LoginController : Controller
    {
        private static readonly HttpClient httpClient = new HttpClient();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                string url = "http://localhost:5277/api/Login";
                var content = new StringContent(JsonConvert.SerializeObject(model), System.Text.Encoding.UTF8, "application/json");

                try
                {
                    var response = await httpClient.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        // Redirige a la página principal
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Usuario o contraseña incorrectos.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = $"Error: {ex.Message}";
                }
            }

            return View(model);
        }
    }
}
