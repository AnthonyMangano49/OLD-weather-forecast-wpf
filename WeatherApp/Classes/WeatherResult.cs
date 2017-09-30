using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherApp.Classes
{
    public class WeatherResult
    {
        public string cityState { get; set; }
        public string zipCode { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string weather { get; set; }
        public string elevation { get; set; }
        public string lastUpdated { get; set; }
        public string temperature { get; set; }
        public string feelsLike { get; set; }
        public string wind { get; set; }
        public string windDirection { get; set; }
        public string humidity { get; set; }
        public string visibility { get; set; }
        public string uv { get; set; }
        public string precipitation { get; set; }
        public string iconURL { get; set; }
        public string icon { get; set; }

    }
}
