using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
//using System.Windows.Shapes;
using WeatherApp.Classes;
using System.IO;

namespace WeatherApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string PlaceholderSearch = "Enter zipcode or city, state";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxSearch.Text.Length == 0)
            {
                return;
            }
            if (textBoxSearch.Text == PlaceholderSearch)
            {
                return;
            }
            WeatherResult wr = WeatherService.GetWeather(textBoxSearch.Text);

            if (wr != null)
            {
                labelCityNameZip.Content = wr.cityState + " " + wr.zipCode;

                labelCityLatLong.Content = "Latitude/Longitude: "
                    + wr.latitude + "/" + wr.longitude;

                labelWeatherCondition.Content = wr.weather;

                labelElevation.Content = "Elevation: " + wr.elevation;

                labelLastUpdated.Content = wr.lastUpdated;

                labelTemperature.Content = "Temperature: " + wr.temperature;

                labelFeelsLike.Content = "Feels Like: " + wr.feelsLike;

                labelWind.Content = "Wind: " + wr.wind;

                labelWindDirection.Content = "Wind Direction: " + wr.windDirection;

                labelHumidity.Content = "Humidity: " + wr.humidity;

                labelVisibility.Content = "Visibility: " + wr.visibility;

                labelUV.Content = "UV: " + wr.uv;

                labelPrecipitation.Content = "Precipitation: " + wr.precipitation;

                setWeatherImage(wr.iconURL, wr.icon);
            }

        }

        private void textBoxSearch_GotFocus(object sender, RoutedEventArgs e)
        {
            var tbox = sender as TextBox;
            tbox.Text = "";
        }

        private void textBoxSearch_LostFocus(object sender, RoutedEventArgs e)
        {
            var tbox = sender as TextBox;
            if (tbox.Text == "")
            {
                tbox.Text = PlaceholderSearch;
            }
        }

        private void setWeatherImage(string imageURL, string imageFileName)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string imageFileDirectory = Path.Combine(Environment.CurrentDirectory, "Images");
                    if (!Directory.Exists(imageFileDirectory))
                    {
                        Directory.CreateDirectory(imageFileDirectory);
                    }

                    string imageFilePath = imageFileDirectory + "\\" + imageFileName;
                    if (!File.Exists(imageFilePath))
                    {
                        webClient.DownloadFile(imageURL, imageFilePath);
                    }
                    
                    var uriSource = new Uri(imageFilePath);
                    imageCondition.Source = new BitmapImage(uriSource);
                }
            }
            catch (Exception)
            {
                var uriSource = new Uri(@"/WeatherApp;component/Images/Sunny.png", UriKind.Relative);
                imageCondition.Source = new BitmapImage(uriSource);
            }

        }
    }
}
