using System.Collections.Generic;
using Azusa.Unity.Util;
using Azusa.WeatherApi.UnitySharedDomain.Enums;
using UnityEngine;

namespace UI
{
    public class WeatherTypeSprite : MonoSingleton<WeatherTypeSprite>
    {
        public static Sprite FromWeatherType(WeatherType weatherType) => Instance.Sprites[weatherType];

        public Dictionary<WeatherType, Sprite> Sprites { get; private set; }
    
        protected override void Awake()
        {
            base.Awake();
            Sprites = new Dictionary<WeatherType, Sprite>()
            {
                { WeatherType.Sunny, Resources.Load<Sprite>("WeatherIcons/Sunny") },
                { WeatherType.Cloudy, Resources.Load<Sprite>("WeatherIcons/Cloudy") },
                { WeatherType.PartlyCloudy , Resources.Load<Sprite>("WeatherIcons/PartlyCloudy")},
                { WeatherType.Rain, Resources.Load<Sprite>("WeatherIcons/Rainy") },
                { WeatherType.HeavyRain , Resources.Load<Sprite>("WeatherIcons/HeavyRainy")},
                { WeatherType.Snow , Resources.Load<Sprite>("WeatherIcons/Snowy")},
                { WeatherType.HeavySnow , Resources.Load<Sprite>("WeatherIcons/HeavySnowy")},
                { WeatherType.Thunder , Resources.Load<Sprite>("WeatherIcons/Thunder")},
            };
        }
    }
}