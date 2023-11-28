namespace Azusa.WeatherApi.WebApi.Dto.AMapGeoCodingApi;

public class AMapGeoCodingApiResponse
{
    public string status { get; set; }
    public string info { get; set; }
    public string infocode { get; set; }
    public string count { get; set; }
    public Geocodes[] geocodes { get; set; }
}

public class Geocodes
{
    public string formatted_address { get; set; }
    public string location { get; set; }
}

