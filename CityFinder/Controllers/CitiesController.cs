using CityFinder.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CityFinder.Controllers
{
    public class CitiesController : Controller
    {

        private const String _baseAddress = "https://api.api-ninjas.com/v1/geocoding?city=";

       // private City defaultCity = new City { Name= "Amman",Latitude= 31.9515694,Longitude= 35.9239625,Country= "JO",State= "Amman" };
        
        [HttpGet]
        public IActionResult SearchCity()
        {
            
            return View(new City { Name = "", Latitude = 0, Longitude = 0, Country = "", State = "" });
        }

        [HttpPost]
        public IActionResult SearchCity(String city) {

            IEnumerable<City> cities = new List<City>();
            using (var client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("X-Api-Key", CityFinder.Configs.Credentials.ApiNinjasKey);
                var response = client.GetAsync(_baseAddress + $"{city}");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var stringData = result.Content.ReadAsStringAsync().Result;
                    cities = JsonConvert.DeserializeObject<IEnumerable<City>>(stringData);
                }
                else
                {
                    ModelState.AddModelError("API Error", "Contact Admin");
                }

            }
            if (cities.Any()) return View(cities.First());
            return View(new City());
        }
    }
}
