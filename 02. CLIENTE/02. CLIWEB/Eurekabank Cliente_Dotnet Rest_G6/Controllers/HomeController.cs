using Eurekabank_Cliente_Dotnet_Rest_G6.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Linq; // Importante para usar LINQ

namespace Eurekabank_Cliente_Dotnet_Rest_G6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private static readonly HttpClient httpClient = new HttpClient();
        private readonly IConfiguration _config; // Se añade para obtener la API Key


        public HomeController(ILogger<HomeController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _config = config;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]

        [HttpGet]
        public async Task<IActionResult> Movimientos(string cuentaNumero)
        {
            if (string.IsNullOrEmpty(cuentaNumero))
            {
                ViewBag.Error = "Por favor, ingresa un número de cuenta.";
                return View();
            }

            string url = $"http://localhost:5277/api/Cuenta/{cuentaNumero}/movimientos";

            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var movimientos = JsonConvert.DeserializeObject<List<Movimiento>>(content);

                    ViewBag.Resultado = movimientos;
                    ViewBag.NumeroCuenta = cuentaNumero;
                }
                else
                {
                    ViewBag.Error = "No se encontraron movimientos para esta cuenta.";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener movimientos");
                ViewBag.Error = $"Ocurrió un error inesperado: {ex.Message}";
            }

            return View();
        }


        public IActionResult Retiro()
        {
            ViewBag.GoogleMapsApiKey = _config["GoogleMaps:ApiKey"];
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Retiro([FromBody] RetiroRequest request)
        {
            if (string.IsNullOrEmpty(request.CuentaNumero) || request.Valor <= 0)
            {
                ViewBag.Mensaje = "Por favor, ingresa datos válidos.";
                ViewBag.CssClass = "alert-danger";
                return View();
            }

            string url = $"http://localhost:5277/api/Cuenta/{request.CuentaNumero}/retiro";
            var contenido = new StringContent(JsonConvert.SerializeObject(request.Valor), System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, contenido);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Retiro realizado con éxito.";
                    ViewBag.CssClass = "alert-success";
                }
                else
                {
                    ViewBag.Mensaje = $"Error al realizar el retiro: {response.ReasonPhrase}";
                    ViewBag.CssClass = "alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar el retiro");
                ViewBag.Mensaje = $"Ocurrió un error inesperado: {ex.Message}";
                ViewBag.CssClass = "alert-danger";
            }

            return View();
        }

        [HttpGet]
        public IActionResult Transferencia()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Transferencia([FromBody] TransferenciaRequest request)
        {
            if (string.IsNullOrEmpty(request.CuentaOrigen) || string.IsNullOrEmpty(request.CuentaDestino) || request.Valor <= 0)
            {
                ViewBag.Mensaje = "Por favor, ingresa datos válidos.";
                ViewBag.CssClass = "alert-danger";
                return View();
            }

            string url = $"http://localhost:5277/api/Cuenta/transferencia";
            var transferenciaRequest = new
            {
                CuentaOrigenCodigo = request.CuentaOrigen,
                CuentaDestinoCodigo = request.CuentaDestino,
                Importe = request.Valor
            };
            var contenido = new StringContent(JsonConvert.SerializeObject(transferenciaRequest), System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, contenido);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Transferencia realizada con éxito.";
                    ViewBag.CssClass = "alert-success";
                }
                else
                {
                    ViewBag.Mensaje = $"Error al realizar la transferencia: {response.ReasonPhrase}";
                    ViewBag.CssClass = "alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar la transferencia");
                ViewBag.Mensaje = $"Ocurrió un error inesperado: {ex.Message}";
                ViewBag.CssClass = "alert-danger";
            }

            return View();
        }


        [HttpGet]
        public IActionResult Deposito()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Deposito([FromBody] DepositoRequest request)
        {
            if (string.IsNullOrEmpty(request.CuentaNumero) || request.Valor <= 0)
            {
                ViewBag.Mensaje = "Por favor, ingresa datos válidos.";
                ViewBag.CssClass = "alert-danger";
                return View();
            }

            string url = $"http://localhost:5277/api/Cuenta/{request.CuentaNumero}/deposito";
            var contenido = new StringContent(JsonConvert.SerializeObject(request.Valor), System.Text.Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await httpClient.PostAsync(url, contenido);

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "Depósito realizado con éxito.";
                    ViewBag.CssClass = "alert-success";
                }
                else
                {
                    ViewBag.Mensaje = $"Error al realizar el depósito: {response.ReasonPhrase}";
                    ViewBag.CssClass = "alert-danger";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al realizar el depósito");
                ViewBag.Mensaje = $"Ocurrió un error inesperado: {ex.Message}";
                ViewBag.CssClass = "alert-danger";
            }

            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Sucursales()
        {
            ViewBag.GoogleMapsApiKey = _config["GoogleMaps:ApiKey"];
            return View();
        }

    }
}
