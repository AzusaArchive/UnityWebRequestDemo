using System;
using Azusa.Extensions;
using UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Views
{
    public class DayWeatherView : MonoBehaviour
    {
        public Text dateText;
        public Image weatherType;
        public Text temperatureText;
        public GameObject selectedMask;
        public RectTransform loading;
        private bool _isSelected;
        private bool _isLoading;
        private Toggle _toggle;
        private ToggleGroup _toggleGroup;

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                SetSelected(value);
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                dateText.enabled = weatherType.enabled = temperatureText.enabled = !value;
                loading.gameObject.SetActive(value);
            }
        }

        public ToggleGroup ToggleGroup
        {
            get { return _toggleGroup; }
            set
            {
                _toggleGroup = value;
                _toggle.group = value;
            }
        }

        public event Action<bool> OnSelected; 

        private void Awake()
        {
            _toggle = gameObject.AddComponent<Toggle>();
            _toggle.onValueChanged.AddListener(SetSelectedNoToggleNotify);
        }

        private void Update()
        {
            loading.Rotate(0, 0, Time.deltaTime * 3);
        }

        private void Start()
        {
            IsSelected = false;   
        }

        public void SetSelected(bool value)
        {
            _isSelected = value;
            if(value)
                _toggle.isOn = true;
            selectedMask.SetActive(value);
            OnSelected?.Invoke(value);
        }

        private void SetSelectedNoToggleNotify(bool value)
        {
            _isSelected = value;
#if UNITY_2021_1_OR_NEWER
            _toggle.SetIsOnWithoutNotify(value);
#endif
            selectedMask.SetActive(value);
            OnSelected?.Invoke(value);
        }

        public void Refresh(DayWeather data)
        {
            dateText.text = $"{data.Date.Day}日 {new DateTime(data.Date.Year, data.Date.Month, data.Date.Day).DayOfWeek.ToStringCHN()}";
            weatherType.sprite = WeatherTypeSprite.FromWeatherType(data.WeatherType);
            temperatureText.text = $"{data.MinTemperature}°~\n{data.MaxTemperature}°";
        }
    }
}