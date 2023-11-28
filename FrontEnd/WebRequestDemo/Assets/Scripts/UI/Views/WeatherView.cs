using System;
using System.Linq;
using Azusa.Extensions;
using Azusa.Unity.Util.Async;
using Azusa.WeatherApi.UnitySharedDomain.Entities;
using Azusa.WeatherApi.UnitySharedDomain.Enums;
using UI.Controllers;
using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    [RequireComponent(typeof(WeatherController))]
    public class WeatherView : MonoBehaviour
    {
        public Loading loading;
        public InputField addressInput;
        public Button searchButton;
        public Toggle isMockToggle;
        public Image weatherType;
        public Text averageTemperatureText;
        public Text maxTemperatureText;
        public Slider maxTemperatureSlider;
        public Text minTemperatureText;
        public Slider minTemperatureSlider;
        public Text dateText;
        public Text rainChanceText;
        public Slider rainChanceSlider;
        public Text windSpeedText;
        public Slider windSpeedSlider;
        public Text humidityText;
        public Slider humiditySlider;
        public Transform weatherCards;

        private float _averageTemperature;
        private float _maxTemperature;
        private float _minTemperature;
        private float _rainChance;
        private float _windSpeed;
        private float _humidity;
        
        private WeatherInfo[] _data;
        private WeatherInfo _weatherInfo;
        private int _selectedDayWeatherViewIndex;
        private DayWeatherView[] _dayWeatherViews;
        private float _lerpDelta = 8;
        private IAsyncTaskBase _task;

        private WeatherController _controller;
        private ToggleGroup _toggleGroup;
        
        private bool IsMock => isMockToggle.isOn;

        private void Awake()
        {
            _dayWeatherViews = new DayWeatherView[7];
            
            _controller = GetComponent<WeatherController>();
            _toggleGroup = gameObject.AddComponent<ToggleGroup>();
            
            foreach (Transform child in weatherCards)
                child.gameObject.SetActive(false);
            for (var i = 0; i < 7; i++)
            {
                _dayWeatherViews[i] = Instantiate(Resources.Load<DayWeatherView>("WeatherCard"), weatherCards);
                _dayWeatherViews[i].ToggleGroup = _toggleGroup;
                var i1 = i;
                _dayWeatherViews[i].OnSelected += value =>
                {
                    if (value)
                    {
                        _selectedDayWeatherViewIndex = i1;
                        RefreshWeatherInfo();
                    }
                };
            }

            searchButton.onClick.AddListener(() => LoadData(addressInput.text));
            loading.OnRetry += () => LoadData(addressInput.text);
            loading.OnCancel += () =>
            {
                loading.Activated = false;
                loading.IsFailed = false;
                _task?.Cancel();
            };
        }

        private void Start()
        {
            _dayWeatherViews[0].IsSelected = true;
            LoadData(addressInput.text);
        }

        private void Update()
        {
            LerpWeatherInfo();
        }

        private void LerpWeatherInfo()
        {
            var averTemp = 0f;
            var maxTemp = 0f;
            var minTemp = 0f;
            var rainChance = 0f;
            var windSpeed = 0f;
            var humidity = 0f;
            if(_weatherInfo != null)
            {
                averTemp = (_weatherInfo.MaxTemperature + _weatherInfo.MinTemperature) / 2f;
                maxTemp = _weatherInfo.MaxTemperature;
                minTemp = _weatherInfo.MinTemperature;
                rainChance = _weatherInfo.RainChance;
                windSpeed = _weatherInfo.WindSpeed;
                humidity = _weatherInfo.Humidity;
            }
            
            _averageTemperature = Mathf.Lerp(_averageTemperature, averTemp, _lerpDelta * Time.deltaTime);
            averageTemperatureText.text = Mathf.RoundToInt(_averageTemperature) + " °C";

            _maxTemperature = Mathf.Lerp(_maxTemperature, maxTemp, _lerpDelta * Time.deltaTime);
            maxTemperatureSlider.value = _maxTemperature;
            maxTemperatureText.text = Mathf.RoundToInt(_maxTemperature) + " °C";

            _minTemperature = Mathf.Lerp(_minTemperature, minTemp, _lerpDelta * Time.deltaTime);
            minTemperatureSlider.value = _minTemperature;
            minTemperatureText.text = Mathf.RoundToInt(_minTemperature) + " °C";

            _rainChance = Mathf.Lerp(_rainChance, rainChance, _lerpDelta * Time.deltaTime);
            rainChanceSlider.value = _rainChance;
            rainChanceText.text = Mathf.RoundToInt(_rainChance) + " %";

            _windSpeed = Mathf.Lerp(_windSpeed, windSpeed, _lerpDelta * Time.deltaTime);
            windSpeedSlider.value = _windSpeed;
            windSpeedText.text = Mathf.RoundToInt(_windSpeed) + " km/h";

            _humidity = Mathf.Lerp(_humidity, humidity, _lerpDelta * Time.deltaTime);
            humiditySlider.value = _humidity;
            humidityText.text = Mathf.RoundToInt(_humidity) + " %";
        }

        public void LoadData(string address)
        {
            loading.Activated = true;
            loading.Text = "正在请求数据";
            _task = _controller.GetRecentWeatherInfoAsync(address, IsMock).WhenCompleted(task =>
            {
                loading.Activated = false;
                _data = task.Result;
                RefreshDayWeathers();
                RefreshWeatherInfo();
            }).WhenInterrupted(task =>
            {
                loading.IsFailed = true;
                loading.Text = $"请求数据失败：{task.ErrorMessage}";
            });
        }

        private void RefreshWeatherInfo()
        {
            if (_data == null)
                return;
            if (_data.Length < 7)
                throw new Exception($"获取到的数据有误，期待数据量: 7，实际数据量{_data.Length}");
            var weatherInfo = _data[_selectedDayWeatherViewIndex];
            _weatherInfo = weatherInfo;
            addressInput.text = weatherInfo.Location;
            weatherType.sprite = WeatherTypeSprite.FromWeatherType(weatherInfo.WeatherType);
            dateText.text =
                $"{weatherInfo.WeatherType.ToStringCHN()} · {weatherInfo.Date.Month}月{weatherInfo.Date.Day}日，{weatherInfo.Date.DayOfWeek.ToStringCHN()}";
        }
        
        private void RefreshDayWeathers()
        {
            if (_data == null)
                return;
            var data = _data.Select(info =>
                new DayWeather(info.Date, info.WeatherType, info.MinTemperature, info.MaxTemperature)).ToArray();
            
            foreach (var view in _dayWeatherViews)
            {
                view.IsLoading = true;
            }

            for (var i = 0; i < (data.Length > 7 ? 7 : data.Length); i++)
            {
                var view = _dayWeatherViews[i];
                view.IsLoading = false;
                view.Refresh(data[i]);
            }
        }
    }
}
