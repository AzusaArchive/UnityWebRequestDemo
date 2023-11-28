using Azusa.WeatherApi.UnitySharedDomain.Entities;
using Azusa.WeatherApi.UnitySharedDomain.Enums;
using Azusa.WeatherApi.WebApi.Dto.CaiYunWeatherApi;

namespace Azusa.WeatherApi.WebApi.Extensions;

public static class DtoMapperExtensions
{
    public static WeatherInfo[] ToWeatherInfo(this CaiYunWeatherApiResponse @this, string location)
    {
        var daily = @this.result.daily;
        var dayCount = daily.temperature.Length;
        var infos = new WeatherInfo[dayCount];
        for (int i = 0; i < dayCount; i++)
        {
            var info = new WeatherInfo
            {
                Date = daily.temperature[i].date,
                Location = location,
                Humidity = (int)(daily.humidity[i].avg * 100),
                RainChance = daily.precipitation[i].probability,
                MaxTemperature = (int)Math.Round(daily.temperature[i].max),
                MinTemperature = (int)Math.Round(daily.temperature[i].min),
                WeatherType = CaiYunWeatherSkycon2WeatherType(daily.skycon[i].value),
                WindSpeed = (int)Math.Round(daily.wind[i].avg.speed)
            };
            infos[i] = info;
        }

        return infos;
    }

    public static WeatherType CaiYunWeatherSkycon2WeatherType(string skycon)
    {
        return skycon switch
        {
            "CLEAR_DAY" or "CLEAR_NIGHT" => WeatherType.Sunny,
            "PARTLY_CLOUDY_DAY" or "PARTLY_CLOUDY_NIGHT" or "LIGHT_HAZE" => WeatherType.PartlyCloudy,
            "CLOUDY" or "MODERATE_HAZE" or "HEAVY_HAZE" or "FOG" => WeatherType.Cloudy,
            "LIGHT_RAIN" or "MODERATE_RAIN" => WeatherType.Rain,
            "HEAVY_RAIN" or "STORM_RAIN" => WeatherType.HeavyRain,
            "LIGHT_SNOW" or "MODERATE_SNOW" => WeatherType.Snow,
            "HEAVY_SNOW" or "STORM_SNOW" => WeatherType.HeavySnow,
            "DUST" or "SAND" => WeatherType.Cloudy,
            "WIND" => WeatherType.Sunny,
            _ => WeatherType.PartlyCloudy
        };
    }
}