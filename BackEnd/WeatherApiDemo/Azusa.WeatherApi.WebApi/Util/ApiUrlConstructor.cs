namespace Azusa.WeatherApi.WebApi.Util;

public static class ApiUrlConstructor
{
    /// <summary>
    /// 构造彩云天气天级别预报Api接口Url
    /// https://docs.caiyunapp.com/docs/daily
    /// </summary>
    /// <param name="apiKey">api密钥</param>
    /// <param name="longitude">经度</param>
    /// <param name="latitude">纬度</param>
    /// <param name="dailyStep">预报天数</param>
    /// <returns></returns>
    public static string GetCaiYunDayWeatherApiUrl(string apiKey, float longitude, float latitude, int dailyStep = 1) =>
        $"https://api.caiyunapp.com/v2.6/{apiKey}/{longitude},{latitude}/daily?dailysteps={dailyStep}";

    /// <summary>
    /// 构造高德地理编码Api接口Url
    /// https://lbs.amap.com/api/webservice/guide/api/georegeo
    /// </summary>
    /// <param name="apiKey">api密钥</param>
    /// <param name="address">规格化地址</param>
    /// <param name="city">搜索城市，留空则全国范围</param>
    /// <returns></returns>
    public static string GetAMapGeoCodingUrl(string apiKey, string address, string? city = null)
    {
        var url = $"https://restapi.amap.com/v3/geocode/geo?key={apiKey}&address={address}";
        if (!string.IsNullOrEmpty(city)) url += $"&city={city}";
        return url;
    }
}