using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace weatherapp.Models
{
    public interface IWeatherRepository
    {
        string[] GetCountries();
        string[] GetCities(string country);
       Task<WeatherData> GetDataAsync(string city);
    }

    public class WeatherRepository : IWeatherRepository
    {
        private static Dictionary<string, string[]> cities = new Dictionary<string, string[]>()
        {
            { "indonesia", new[] { "Jakarta", "Malang", "Semarang", "Surabaya", "Bandung" } },
            { "malaysia", new[] { "Kelantan", "Pulau Pinang", "Kedah", "Johor", "Perak" } },
            { "thailand", new[] { "Tamot", "Tan Sum", "Bangkok", "Tha Luan", "Tha Mai" } },

            { "australia", new[] { "Wembley", "Wellington", "Wellard", "Spearwood", "Springton" } },
            { "vietnam", new[] { "Uong Bi", "Tuy Hoa", "Vinh", "Yen Bai", "Thanh Hoa" } },
        };

        private readonly string base_url = @"http://api.openweathermap.org";
        private readonly string api_key = "bdb453c274f0f9d93cca4721d5efa335";
        public string[] GetCountries()
        {
            return cities.Keys.ToArray();
        }

        public string[] GetCities(string country)
        {
            if(cities.ContainsKey(country.ToLower()))
                return cities[country.ToLower()];
            return Array.Empty<string>();
        }

        public async Task<WeatherData> GetDataAsync(string city)
        {
            try
            {
               
                HttpClient client = new HttpClient();
                using HttpResponseMessage responseMessage =
                    await client.GetAsync(base_url + @$"/data/2.5/weather?q={city}&appid={api_key}&units=imperial");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string response = await responseMessage.Content.ReadAsStringAsync();
                    return WeatherData.FromJson(response);
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
                
            }
        }
    }
}