using Azusa.WeatherApi.UnitySharedDomain.Enums;
using Azusa.WeatherApi.UnitySharedDomain.Premitives;

namespace UI.Models
{
    public struct DayWeather
    {
        public DateOnly Date { get; set; }
        public WeatherType WeatherType { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }

        public DayWeather(DateOnly date, WeatherType weatherType, int minTemperature, int maxTemperature)
        {
            Date = date;
            WeatherType = weatherType;
            MinTemperature = minTemperature;
            MaxTemperature = maxTemperature;
        }
    }
}