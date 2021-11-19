using System.Collections.Generic;

namespace WeatherApplication.Models.CurrentWeather
{ 
    public class CurrentWeather
    {
        public Coord coord { get; set; }
        public List<Weather> weather { get; set; }
        public string @base { get; set; }
        public Main main { get; set; }
        public int visibility { get; set; }
        public Wind wind { get; set; }
        public Clouds clouds { get; set; }
        public int dt { get; set; }
        public Sys sys { get; set; }
        public int timezone { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public int cod { get; set; }

        public override string ToString()
        {
            return $"Weather \nTemp: {main.temp} °C \tFeels like: {main.feels_like} °C\nWind speed: {wind.speed} \nWether: {weather[0].description} \t{clouds.all}%";
        }
    }
}
