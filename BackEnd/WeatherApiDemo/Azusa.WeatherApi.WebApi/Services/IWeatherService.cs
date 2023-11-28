using Azusa.WeatherApi.UnitySharedDomain.Entities;

namespace Azusa.WeatherApi.WebApi.Services;

public interface IWeatherService
{
    Task<WeatherInfo[]> GetWeatherInfoAsync(string address);
}