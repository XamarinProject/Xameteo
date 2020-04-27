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
        public bool CityDetails { set; get; }
        public bool SunsetSunriseDetails { set; get; }
        public bool TemperatureDetails { set; get; }

    }
}
