using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WeatherWebApi.Infrastructure
{
    public interface IWeatherForecastRepository
    {
        Task<List<WeatherForecast>> GetAll();

        Task<WeatherForecast> Add(WeatherForecast forecast);
    }
}