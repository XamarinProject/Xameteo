using Newtonsoft.Json;
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
using Xameteo.Models;

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

        public static async Task<CompleteWeather> GetWeatherAndForecast(string name)
        {
            var result = await Get($"http://api.openweathermap.org/data/2.5/forecast?q={name}&appid=35ff7a413590bd1a0c52e0486cdbed66");
            return JsonConvert.DeserializeObject<CompleteWeather>(result);
        }
    }
}
