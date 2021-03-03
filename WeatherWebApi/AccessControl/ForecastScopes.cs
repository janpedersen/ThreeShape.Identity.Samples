namespace WeatherWebApi.AccessControl
{
    public static class ForecastScopes
    {
        public static string ReadOnly => "weather.forecast.read";
        public static string ManageAccess => "weather.forecast.manage";
    }
}