namespace WeatherApplication.Models.CurrentWeather
{
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        private string icon;

        public string Icon
        {
            get { return icon; }
            set { icon = $"http://openweathermap.org/img/wn/{value}@2x.png"; }
        }
    }
}
