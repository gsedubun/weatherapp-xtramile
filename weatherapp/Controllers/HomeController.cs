using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using weatherapp.Models;

namespace weatherapp.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : ControllerBase
    {
        private readonly IWeatherRepository _weatherRepository;

        public HomeController(IWeatherRepository weatherRepository)
        {
            _weatherRepository = weatherRepository;
        }

        [HttpGet]
        public IActionResult countries()
        {
            var c = _weatherRepository.GetCountries();
            if (c.Length > 0)
            {
                return Ok(c);
            }

            return NoContent();
        }

        [HttpGet]
        public IActionResult cities(string country)
        {
            var c = _weatherRepository.GetCities(country);
            if (c.Length > 0)
            {
                return Ok(c);
            }

            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> weather(string city)
        {
            try
            {
                var c = await _weatherRepository.GetDataAsync(city);
                if (c != null)
                {
                    return Ok(c);
                }

                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
    }
}