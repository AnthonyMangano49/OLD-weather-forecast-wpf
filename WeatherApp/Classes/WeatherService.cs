using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Windows;

namespace WeatherApp.Classes
{
    public class WeatherService
    {
        public static WeatherResult GetWeather(string input)
        {
            string url = "http://api.wunderground.com/api/56e3bd34466c5206/conditions/q/"
                + input + ".json";

            try
            {
                using (WebClient webClient = new WebClient())
                {
                    string json = webClient.DownloadString(url);

                    var o = JObject.Parse(json);

                    var oResponse = o["response"];

                    var oError = oResponse["error"];

                    if (oError != null)
                    {
                        MessageBox.Show("No data found for " + input);
                        return null;
                    }
                    var oResults = oResponse["results"];
                    if (oResults != null)
                    {
                        MessageBox.Show("Unable to find results for " + input + ". Please search with zipcode or city, state.");
                        return null;
                    }

                    var oCurrentObservation = o["current_observation"];

                    var oDisplayLocation = oCurrentObservation["display_location"];

                    WeatherResult wResult = new WeatherResult();

                    wResult.cityState = oDisplayLocation["full"].ToString();

                    wResult.zipCode = oDisplayLocation["zip"].ToString();

                    wResult.longitude = oDisplayLocation["longitude"].ToString();

                    wResult.latitude = oDisplayLocation["latitude"].ToString();

                    wResult.weather = oCurrentObservation["weather"].ToString();

                    wResult.elevation = oDisplayLocation["elevation"].ToString();

                    wResult.lastUpdated = oCurrentObservation["observation_time"].ToString();

                    wResult.temperature = oCurrentObservation["temperature_string"].ToString();

                    wResult.feelsLike = oCurrentObservation["feelslike_string"].ToString();

                    wResult.wind = oCurrentObservation["wind_string"].ToString();

                    wResult.windDirection = oCurrentObservation["wind_dir"].ToString();

                    wResult.humidity = oCurrentObservation["relative_humidity"].ToString();

                    wResult.visibility = oCurrentObservation["visibility_mi"].ToString();

                    wResult.uv = oCurrentObservation["UV"].ToString();

                    wResult.precipitation = oCurrentObservation["precip_today_string"].ToString();

                    wResult.iconURL = oCurrentObservation["icon_url"].ToString();

                    wResult.icon = oCurrentObservation["icon"].ToString();

                    return wResult;
                }
            }

            catch (WebException)
            {
                MessageBox.Show("No weather data found for {0}", input);
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error occured: " + url + " " + e.Message);
                return null;
            }

        }
    }
}