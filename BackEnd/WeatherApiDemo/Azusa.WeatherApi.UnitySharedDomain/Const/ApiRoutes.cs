namespace Azusa.WeatherApi.UnitySharedDomain.Const
{
    public static class ApiRoutes
    {
        public static class Weather
        {
            private const string Base = "/Weather";
            public const string AMap = Base + "/AMap";
            public const string Mock = Base + "/Mock";
        }
    }
}