using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeatherWebApi.Infrastructure
{
    public class WeatherForecastRepository : IWeatherForecastRepository
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly Dictionary<DateTime, WeatherForecast> _weatherForecasts;

        public WeatherForecastRepository()
        {
            var rng = new Random();
            _weatherForecasts = new Dictionary<DateTime, WeatherForecast>(
                Enumerable.Range(1, 5)
                        .Select(index => KeyValuePair.Create(DateTime.Now.AddDays(index).Date,
                            new WeatherForecast
                            {
                                Date = DateTime.Now.AddDays(index),
                                TemperatureC = rng.Next(-20, 55),
                                Summary = Summaries[rng.Next(Summaries.Length)]
                            }))
                        .ToArray());
        }

        public Task<List<WeatherForecast>> GetAll()
        {
            return Task.FromResult(_weatherForecasts.Values.ToList());
        }

        public Task<WeatherForecast> Add(WeatherForecast forecast)
        {
            _weatherForecasts.Add(forecast.Date.Date, forecast);
            return Task.FromResult(forecast);
        }
    }
}