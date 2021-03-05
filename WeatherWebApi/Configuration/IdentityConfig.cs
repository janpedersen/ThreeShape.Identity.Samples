namespace WeatherWebApi.AccessControl
{
    public class IdentityConfig
    {
        public string AuthEndpoint { get; set; }
        public bool RequireCompany { get; set; }
        public bool RequireHttps { get; set; }
    }
}