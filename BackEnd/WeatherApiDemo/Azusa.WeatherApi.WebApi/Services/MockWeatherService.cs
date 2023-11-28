using Azusa.WeatherApi.UnitySharedDomain.Entities;
using Azusa.WeatherApi.UnitySharedDomain.Enums;
using DateOnly = Azusa.WeatherApi.UnitySharedDomain.Premitives.DateOnly;

namespace Azusa.WeatherApi.WebApi.Services;

/// <summary>
/// 模拟天气服务
/// </summary>
public class MockWeatherService : IWeatherService
{
    public static WeatherInfo[] ExampleData =
    {
        new WeatherInfo()
        {
            Date = new DateOnly(2023, 10, 18),
            Humidity = 84,
            Location = "福州市闽侯县",
            RainChance = 0,
            MaxTemperature = 29,
            MinTemperature = 19,
            WeatherType = WeatherType.PartlyCloudy,
            WindSpeed = 5,
        },
        new WeatherInfo()
        {
            Date = new DateOnly(2023, 10, 19),
            Humidity = 65,
            Location = "福州市闽侯县",
            RainChance = 0,
            MaxTemperature = 29,
            MinTemperature = 22,
            WeatherType = WeatherType.Sunny,
            WindSpeed = 5,
        },
        new WeatherInfo()
        {
            Date = new DateOnly(2023, 10, 20),
            Humidity = 96,
            Location = "福州市闽侯县",
            RainChance = 78,
            MaxTemperature = 25,
            MinTemperature = 18,
            WeatherType = WeatherType.Rain,
            WindSpeed = 6,
        },
        new WeatherInfo()
        {
            Date = new DateOnly(2023, 10, 21),
            Humidity = 52,
            Location = "福州市闽侯县",
            RainChance = 2,
            MaxTemperature = 22,
            MinTemperature = 18,
            WeatherType = WeatherType.PartlyCloudy,
            WindSpeed = 6,
        },
        new WeatherInfo()
        {
            Date = new DateOnly(2023, 10, 22),
            Humidity = 58,
            Location = "福州市闽侯县",
            RainChance = 0,
            MaxTemperature = 25,
            MinTemperature = 18,
            WeatherType = WeatherType.PartlyCloudy,
            WindSpeed = 6,
        },
        new WeatherInfo()
        {
            Date = new DateOnly(2023, 10, 23),
            Humidity = 63,
            Location = "福州市闽侯县",
            RainChance = 0,
            MaxTemperature = 25,
            MinTemperature = 20,
            WeatherType = WeatherType.Cloudy,
            WindSpeed = 5,
        },
        new WeatherInfo()
        {
            Date = new DateOnly(2023, 10, 24),
            Humidity = 68,
            Location = "福州市闽侯县",
            RainChance = 0,
            MaxTemperature = 27,
            MinTemperature = 20,
            WeatherType = WeatherType.PartlyCloudy,
            WindSpeed = 6,
        },
        new WeatherInfo()
        {
            Date = new DateOnly(2023, 10, 25),
            Humidity = 67,
            Location = "福州市闽侯县",
            RainChance = 1,
            MaxTemperature = 28,
            MinTemperature = 20,
            WeatherType = WeatherType.PartlyCloudy,
            WindSpeed = 6,  
        },
    };
    
    public Task<WeatherInfo[]> GetWeatherInfoAsync(string? address = null)
    {
        return Task.FromResult(ExampleData);
    }
}