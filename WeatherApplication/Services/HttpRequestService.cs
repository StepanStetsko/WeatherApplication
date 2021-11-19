using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApplication.Models;
using WeatherApplication.Models.CurrentWeather;
using WeatherApplication.Models.ForecastWeather;

namespace WeatherApplication.Services
{
    internal class HttpRequestService
    {
        private readonly HttpClient httpClient;
        private readonly string apiKey = "57a334b0f068bf4c0fc13f01d3ef5f8b";
        private readonly LoggerConfiguration logger = new LoggerConfiguration();
        private List<CurrentCityForecast> cityForecast;
        private HttpResponseMessage weatherResponse;

        public HttpRequestService()
        {
            httpClient = new HttpClient();
            var a = Directory.GetCurrentDirectory() + @"\Logs\log.txt";
            Log.Logger = new LoggerConfiguration().WriteTo.File(a, rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true)
                 .CreateLogger();
        }

        public async Task<CurrentWeather> GetCurrentWeather(string city)
        {
            CurrentWeather currentWeather = null;
            var requestUri = $"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID={apiKey}&units=metric";

            try
            {
                weatherResponse = await httpClient.GetAsync(requestUri);
                currentWeather = JsonConvert.DeserializeObject<CurrentWeather>(weatherResponse.Content.ReadAsStringAsync().Result);
            }
            catch (Exception ex)
            {
                Log.Fatal($"{ex.Message}");
            }

            Log.Information($"Request: {requestUri} Response code {weatherResponse.StatusCode}");
            return currentWeather;
        }
        public async Task<List<CurrentCityForecast>> GetForecastWeather(string city)
        {
            var requestUri = $"http://api.openweathermap.org/data/2.5/weather?q={city}&APPID={apiKey}&units=metric";

            try
            {
                var weatherResponse = await httpClient.GetAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&appid={apiKey}&units=metric");
                var forecastWeather = JsonConvert.DeserializeObject<ForecastWeather>(weatherResponse.Content.ReadAsStringAsync().Result);

                List<Models.ForecastWeather.List> fiveDayForecast = new List<Models.ForecastWeather.List>();
                cityForecast = new List<CurrentCityForecast>();

                for (int i = 0; i < 5; i++)
                {
                    var date = DateTime.Now.AddDays(i).ToString("yyyy-MM-dd");
                    var temp = forecastWeather.list.FindAll(x => x.dt_txt.Split(' ')[0] == date);

                    cityForecast.Add(new CurrentCityForecast
                    {
                        TempMin = Math.Round(temp.Select(x => x.main.temp_min).Min()).ToString(),
                        TempMax = Math.Round(temp.Select(x => x.main.temp_max).Max()).ToString(),
                        WindSpeed = Math.Round(temp.Select(x => x.wind.speed).Average(), 2).ToString(),
                        WeatherDate = temp[0].dt_txt.Split(' ')[0],
                        IconPath = temp[0].weather[0].icon
                    });
                }
            }
            catch (Exception ex)
            {
                Log.Fatal($"{ex.Message}");
            }

            Log.Information($"Request: {requestUri} Response code {weatherResponse.StatusCode}");

            return cityForecast;
        }
    }
}