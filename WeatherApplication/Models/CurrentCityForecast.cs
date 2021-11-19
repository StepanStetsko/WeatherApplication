using System;

namespace WeatherApplication.Models
{
    internal class CurrentCityForecast
    {
        public string tempMin;
        public string TempMin
        {
            get { return tempMin; }
            set { tempMin = $"Min: {value} °C"; }
        }
        
        public string tempMax;
        public string TempMax
        {
            get { return tempMax; }
            set { tempMax = $"Max: {value} °C"; }
        }

        private string windSpeed;
        public string WindSpeed
        {
            get { return windSpeed; }
            set { windSpeed = $"Wind speed: {value}"; }
        }

        private string weatherDate;
        public string WeatherDate
        {
            get { return weatherDate; }
            set { weatherDate = $"Date: {value}"; }
        }

        private string iconPath;
        public string IconPath
        {
            get { return iconPath; }
            set { iconPath = $"http://openweathermap.org/img/wn/{value}@2x.png"; }
        }

    }
}
