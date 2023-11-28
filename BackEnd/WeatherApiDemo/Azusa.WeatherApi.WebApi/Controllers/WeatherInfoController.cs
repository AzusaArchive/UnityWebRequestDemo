using Azusa.WeatherApi.UnitySharedDomain.Const;
using Azusa.WeatherApi.UnitySharedDomain.Entities;
using Azusa.WeatherApi.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Azusa.WeatherApi.WebApi.Controllers;

[Controller]
public class WeatherInfoController : ControllerBase
{
    private readonly MockWeatherService _mockWeather;
    private readonly CaiYunWeatherService _amapWeather;

    public WeatherInfoController(MockWeatherService mockWeather, CaiYunWeatherService amapWeather)
    {
        _mockWeather = mockWeather;
        _amapWeather = amapWeather;
    }

    /// <summary>
    /// 获取天气信息（模拟数据）
    /// </summary>
    /// <returns></returns>
    [HttpGet(ApiRoutes.Weather.Mock)]
    public async Task<ActionResult<WeatherInfo[]>> GetMockWeatherInfo()
    {
        return await _mockWeather.GetWeatherInfoAsync();
    }

    /// <summary>
    /// 获取天气信息（彩云API）
    /// </summary>
    /// <param name="address">地址</param>
    /// <returns></returns>
    [HttpGet(ApiRoutes.Weather.AMap)]
    public async Task<ActionResult<WeatherInfo[]>> GetAMapWeatherInfo([FromQuery] string address)
    {
        if (string.IsNullOrWhiteSpace(address))
            return BadRequest("请求地址为空");
        return await _amapWeather.GetWeatherInfoAsync(address);
    }
}