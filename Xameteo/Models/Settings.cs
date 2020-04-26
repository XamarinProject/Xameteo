using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Xameteo.Models
{
    public class Settings
    {
        public Settings()
        {
            CityDetails = true;
            SunsetSunriseDetails = true;
            TemperatureDetails = true;
        }
        [JsonProperty("CityDetails")]
        public bool CityDetails { set; get; }
        [JsonProperty("SunsetSunriseDetails")]
        public bool SunsetSunriseDetails { set; get; }
        [JsonProperty("TemperatureDetails")]
        public bool TemperatureDetails { set; get; }

    }
}
