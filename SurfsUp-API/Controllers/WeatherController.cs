using Microsoft.AspNetCore.Mvc;
using SurfsUp_Models;

namespace SurfsUp_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly SurfsUpContext _context;
        private readonly HttpClient _http;
        private readonly string? _apiKey;

        public WeatherController(SurfsUpContext context, HttpClient http)
        {
            _context = context;
            _http = http;
            _apiKey = Environment.GetEnvironmentVariable("OPENWEATHERAPI_KEY");
        }

        [HttpGet("Read", Name = "Get Weather")]
        public async Task<ActionResult> Get([FromQuery] string location)
        {
            Root? forecasts = null;
            var city = await _http.GetFromJsonAsync<SRoot>($"https://openweathermap.org/data/2.5/find?q={location}&appid=439d4b804bc8187953eb36d2a8c26a02");
            if (city != null)
            {
                forecasts = await _http.GetFromJsonAsync<Root>($"https://api.openweathermap.org/data/2.5/forecast?id={city.list.First().id}&appid={_apiKey}");
            }
            return Ok(forecasts);
        }
    }
}
