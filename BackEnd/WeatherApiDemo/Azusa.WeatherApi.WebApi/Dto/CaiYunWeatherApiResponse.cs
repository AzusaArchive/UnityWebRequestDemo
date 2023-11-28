namespace Azusa.WeatherApi.WebApi.Dto.CaiYunWeatherApi;

public class CaiYunWeatherApiResponse
{
    public string status { get; set; }
    public string api_version { get; set; }
    public string api_status { get; set; }
    public string lang { get; set; }
    public string unit { get; set; }
    public int tzshift { get; set; }
    public string timezone { get; set; }
    public int server_time { get; set; }
    public double[] location { get; set; }
    public Result result { get; set; }
}

public class Result
{
    public Daily daily { get; set; }
    public int primary { get; set; }
}

public class Daily
{
    public string status { get; set; }
    public Astro[] astro { get; set; }
    public Precipitation_08h_20h[] precipitation_08h_20h { get; set; }
    public Precipitation_20h_32h[] precipitation_20h_32h { get; set; }
    public Precipitation[] precipitation { get; set; }
    public Temperature[] temperature { get; set; }
    public Temperature_08h_20h[] temperature_08h_20h { get; set; }
    public Temperature_20h_32h[] temperature_20h_32h { get; set; }
    public Wind[] wind { get; set; }
    public Wind_08h_20h[] wind_08h_20h { get; set; }
    public Wind_20h_32h[] wind_20h_32h { get; set; }
    public Humidity[] humidity { get; set; }
    public Cloudrate[] cloudrate { get; set; }
    public Pressure[] pressure { get; set; }
    public Visibility[] visibility { get; set; }
    public Dswrf[] dswrf { get; set; }
    public Air_quality air_quality { get; set; }
    public Skycon[] skycon { get; set; }
    public Skycon[] skycon_08h_20h { get; set; }
    public Skycon[] skycon_20h_32h { get; set; }
    public Life_index life_index { get; set; }
}

public class Astro
{
    public string date { get; set; }
    public Sunrise sunrise { get; set; }
    public Sunset sunset { get; set; }
}

public class Sunrise
{
    public string time { get; set; }
}

public class Sunset
{
    public string time { get; set; }
}

public class Precipitation_08h_20h
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
    public int probability { get; set; }
}

public class Precipitation_20h_32h
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
    public int probability { get; set; }
}

public class Precipitation
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
    public int probability { get; set; }
}

public class Temperature
{
    public DateTime date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
}

public class Temperature_08h_20h
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
}

public class Temperature_20h_32h
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
}

public class Wind
{
    public string date { get; set; }
    public Max max { get; set; }
    public Min min { get; set; }
    public Avg avg { get; set; }
}

public class Max
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Min
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Avg
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Wind_08h_20h
{
    public string date { get; set; }
    public Max1 max { get; set; }
    public Min1 min { get; set; }
    public Avg1 avg { get; set; }
}

public class Max1
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Min1
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Avg1
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Wind_20h_32h
{
    public string date { get; set; }
    public Max2 max { get; set; }
    public Min2 min { get; set; }
    public Avg2 avg { get; set; }
}

public class Max2
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Min2
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Avg2
{
    public double speed { get; set; }
    public double direction { get; set; }
}

public class Humidity
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
}

public class Cloudrate
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
}

public class Pressure
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
}

public class Visibility
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
}

public class Dswrf
{
    public string date { get; set; }
    public double max { get; set; }
    public double min { get; set; }
    public double avg { get; set; }
}

public class Air_quality
{
    public Aqi[] aqi { get; set; }
    public Pm25[] pm25 { get; set; }
}

public class Aqi
{
    public string date { get; set; }
    public Max3 max { get; set; }
    public Avg3 avg { get; set; }
    public Min3 min { get; set; }
}

public class Max3
{
    public int chn { get; set; }
    public int usa { get; set; }
}

public class Avg3
{
    public int chn { get; set; }
    public int usa { get; set; }
}

public class Min3
{
    public int chn { get; set; }
    public int usa { get; set; }
}

public class Pm25
{
    public string date { get; set; }
    public int max { get; set; }
    public int avg { get; set; }
    public int min { get; set; }
}

public class Skycon
{
    public string date { get; set; }
    public string value { get; set; }
}

public class Life_index
{
    public Ultraviolet[] ultraviolet { get; set; }
    public CarWashing[] carWashing { get; set; }
    public Dressing[] dressing { get; set; }
    public Comfort[] comfort { get; set; }
    public ColdRisk[] coldRisk { get; set; }
}

public class Ultraviolet
{
    public string date { get; set; }
    public string index { get; set; }
    public string desc { get; set; }
}

public class CarWashing
{
    public string date { get; set; }
    public string index { get; set; }
    public string desc { get; set; }
}

public class Dressing
{
    public string date { get; set; }
    public string index { get; set; }
    public string desc { get; set; }
}

public class Comfort
{
    public string date { get; set; }
    public string index { get; set; }
    public string desc { get; set; }
}

public class ColdRisk
{
    public string date { get; set; }
    public string index { get; set; }
    public string desc { get; set; }
}

