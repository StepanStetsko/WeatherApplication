using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using WeatherApplication.Models.CurrentWeather;
using WeatherApplication.Services;

namespace WeatherApplication
{
    public partial class MainWindow : Window
    {
        private readonly HttpWeatherService httpRequest;
        private CurrentWeather currentCity;

        public MainWindow()
        {
            httpRequest = new HttpWeatherService();

            InitializeComponent();
            var defaultCity = (citySelection.SelectedItem as TextBlock).Text;

            UpdateWeather(defaultCity);
        }
        public async void UpdateWeather(string city)
        {
            var currentCityResponce = await httpRequest.GetCurrentWeather(city);
            var forecastWeatherResponce = await httpRequest.GetForecastWeather(city);

            if (forecastWeatherResponce != null)
            {
                currentCity = currentCityResponce;
                forecastWeather.ItemsSource = forecastWeatherResponce;
                var a = currentCity.weather[0].Icon;
                weatherIcon.Source = new BitmapImage(new Uri(a));
                currentCityName.Text = currentCity.ToString();
            }
        }

        private void citySelection_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var currentCityResponce = (comboBox.SelectedItem as TextBlock).Text;

            UpdateWeather(currentCityResponce);
        }
    }
}
