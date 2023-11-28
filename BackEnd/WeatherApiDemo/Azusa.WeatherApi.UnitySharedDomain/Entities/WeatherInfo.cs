using Azusa.WeatherApi.UnitySharedDomain.Enums;
using Azusa.WeatherApi.UnitySharedDomain.Premitives;

namespace Azusa.WeatherApi.UnitySharedDomain.Entities
{
    public class WeatherInfo
    {
        public string Location { get; set; }
        public WeatherType WeatherType { get; set; }
        public int MaxTemperature { get; set; }
        public int MinTemperature { get; set; }
        public int RainChance { get; set; }
        public int WindSpeed { get; set; }
        public int Humidity { get; set; }
        public DateOnly Date { get; set; }
    }
}