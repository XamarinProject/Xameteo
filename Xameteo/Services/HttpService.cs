﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Xameteo
{
    public class HttpService : HttpClient
    {
        public static async Task<List<City>> GetCities(string city)
        {
            string json = await Get($"http://geodb-free-service.wirefreethought.com/v1/geo/cities?namePrefix={city}");
            CityResponse cityResponse = JsonConvert.DeserializeObject<CityResponse>(json);
            return cityResponse.Cities;
        }

        /*public static async Task<Weather> GetWeather(string city)
        {
            string json = await Get($"https://api.openweathermap.org/data/2.5/weather?{city}");
            Weather weather = JsonConvert.DeserializeObject<WeatherResponse>(json);
            return weather;
        }*/

        public static Task<string> Get(string url)
        {
            var headers = new WebHeaderCollection();
            var request = HttpWebRequest.Create(url);
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Headers = headers;

            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Shell.Current.DisplayAlert("Erreur", $"Le Serveur a retourné le code: {response.StatusCode}.", "OK");
                        return;
                    });
                }
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string content = reader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            await Shell.Current.DisplayAlert("Erreur", "La réponse du serveur est vide.", "OK");
                            return;
                        });
                    }
                    
                    return Task.FromResult(content);
                }
            }
        }
    }
}
