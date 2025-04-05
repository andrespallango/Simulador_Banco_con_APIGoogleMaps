using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace Eurekabank_Cliente_Dotnet_Rest_G6.Controllers
{
    [ApiController]
    [Route("api/maps")]
    public class MapsController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient;

        public MapsController(IConfiguration config, HttpClient httpClient)
        {
            _config = config;
            _httpClient = httpClient;
        }

        [HttpGet("coordinates")]
        public async Task<IActionResult> GetCoordinates(string address)
        {
            var apiKey = _config["GoogleMaps:ApiKey"];
            var url = $"https://maps.googleapis.com/maps/api/geocode/json?address={address}&key={apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            return Ok(response);
        }

        [HttpGet("place")]
        public async Task<IActionResult> GetPlaceDetails(string placeId)
        {
            var apiKey = _config["GoogleMaps:ApiKey"];
            var url = $"https://maps.googleapis.com/maps/api/place/details/json?place_id={placeId}&key={apiKey}";
            var response = await _httpClient.GetStringAsync(url);
            return Ok(response);
        }
    }
}
