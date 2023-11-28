using System;

namespace Azusa.WeatherApi.UnitySharedDomain.Enums
{
    public enum WeatherType
    {
        Sunny,
        Cloudy,
        PartlyCloudy,
        Rain,
        HeavyRain,
        Snow,
        HeavySnow,
        Thunder,
    }

    public static class WeatherTypeExtensions
    {
        public static string ToStringCHN(this WeatherType @this) => @this switch
        {
            WeatherType.Sunny => "晴",
            WeatherType.Cloudy => "阴",
            WeatherType.PartlyCloudy => "多云",
            WeatherType.Rain => "有雨",
            WeatherType.HeavyRain => "大雨",
            WeatherType.Snow => "雪",
            WeatherType.HeavySnow => "大雪",
            WeatherType.Thunder => "雷电",
            _ => throw new ArgumentOutOfRangeException(nameof(@this), @this, null)
        };
    }
}