using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WeatherWebApi.Controllers
{
    [AllowAnonymous]
    [Route("api/.discover")]
    public class DiscoveryController : ControllerBase
    {
        [HttpGet("api-configuration/v1")]
        public WeatherApiDiscovery GetV1ApiConfiguration()
        {
            var baseUrl = $"{Request.Scheme}://{Request.Host}";

            return new WeatherApiDiscovery
            {
                WeatherForecastEndpoint = $"{baseUrl}/api/v1/weatherForecast",
            };
        }
    }
}