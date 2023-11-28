using Azusa.WeatherApi.UnitySharedDomain.Entities;
using Azusa.WeatherApi.WebApi.Dto.AMapGeoCodingApi;
using Azusa.WeatherApi.WebApi.Dto.CaiYunWeatherApi;
using Azusa.WeatherApi.WebApi.Extensions;
using Azusa.WeatherApi.WebApi.Primitives.Options;
using Azusa.WeatherApi.WebApi.Util;
using Microsoft.Extensions.Options;

namespace Azusa.WeatherApi.WebApi.Services;

/// <summary>
/// 使用彩云天气服务获取天气信息
/// </summary>
public class CaiYunWeatherService : IWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly IOptions<ApiKeysOptions> _apiKeys;

    public CaiYunWeatherService(HttpClient httpClient, IOptions<ApiKeysOptions> apiKeys)
    {
        _httpClient = httpClient;
        _apiKeys = apiKeys;
    }

    public async Task<WeatherInfo[]> GetWeatherInfoAsync(string address)
    {
        // 借助高德地图地理编码API将地址转换为经纬度坐标
        var geo = await _httpClient.GetFromJsonAsync<AMapGeoCodingApiResponse>(
            ApiUrlConstructor.GetAMapGeoCodingUrl(_apiKeys.Value.AMapApiKey, address));

        if (geo is null)
            throw new ApplicationException("服务端API请求失败");
        if (geo.status is "0")
            if (geo.infocode is "30001")
                throw new ArgumentException($"AMap ERROR:30001，请求的地址\"{address}\"有误");
            else throw new ApplicationException($"AMap ERROR:{geo.infocode}，服务端API请求失败");
        if (geo.geocodes.Length is 0)
            throw new ArgumentException($"请求的地址\"{address}\"有误");

        var addrInfo = geo.geocodes[0];
        var addrFullName = addrInfo.formatted_address;

        var locStr = addrInfo.location.Split(',');
        (float longitude, float latitude) location = (float.Parse(locStr[0]), float.Parse(locStr[1]));

        // 访问彩云天气API根据经纬度获取天气预报信息
        var info = await _httpClient.GetFromJsonAsync<CaiYunWeatherApiResponse>(
            ApiUrlConstructor.GetCaiYunDayWeatherApiUrl(_apiKeys.Value.CaiYunWeatherApiKey, location.longitude,
                location.latitude, 7));
        
        if (info?.status is not "ok")
            throw new ApplicationException("服务端API请求失败");

        return info.ToWeatherInfo(addrFullName);
    }
}