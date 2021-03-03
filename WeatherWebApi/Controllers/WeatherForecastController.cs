using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WeatherWebApi.AccessControl;
using WeatherWebApi.Infrastructure;

namespace WeatherWebApi.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IWeatherForecastRepository _forecastRepository;
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserAuthorizationFactory _userAuthorizationFactory;

        public WeatherForecastController(IWeatherForecastRepository forecastRepository,
            ILogger<WeatherForecastController> logger,
            UserAuthorizationFactory userAuthorizationFactory)
        {
            _forecastRepository = forecastRepository;
            _logger = logger;
            _userAuthorizationFactory = userAuthorizationFactory;
        }

        [HttpGet]
        //Policies specify what scopes are needed for accessing this endpoint
        [Authorize(Policy = Policies.ForecastRead)]
        public async Task<IEnumerable<WeatherForecast>> GetAll()
        {
            //getting user
            var user = _userAuthorizationFactory.GetAuthorizedUser();
            //can user it in whatever way, pass further along to do fine grained authorization inside your application
            Debug.WriteLine($"Does this user have a role {RoleNames.EmployeeAdministrator}: {user.ContainsRole(RoleNames.EmployeeAdministrator)}");
            return await _forecastRepository.GetAll();
        }

        [HttpPost]
        //You can specify (by asking ID team to give scopes to some particular clients) that a certain client
        //can have access to a priveledged endpoint, e.g. only 3Shape internal clients can access this endpoint
        [Authorize(Policy = Policies.ForecastManage)]
        public async Task<WeatherForecast> Add(WeatherForecast newForecast)
        {
            return await _forecastRepository.Add(newForecast);
        }
    }
}