using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Xameteo
{
    public class CityResponse
    {
        [JsonProperty("data")]
        public List<City> Cities { get; set; }
    }

    public class City
    {
        [JsonProperty("wikiDataId")]
        public string WikiDataId { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("countryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("region")]
        public string Region { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("temperature")]
        public double Temperature { get; set; }

        [JsonIgnore]
        public string FullName
        {
            get { return $"{Name}, {Region}, {Country}"; }
        }

        [JsonIgnore]
        public string FullTemperature
        {
            get { return $"{Temperature}°C"; }
        }

        [JsonProperty]
        public bool IsFavorite { get; set; }
    }
}
