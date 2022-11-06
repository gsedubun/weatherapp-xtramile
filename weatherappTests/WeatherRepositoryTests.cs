using Moq;
using weatherapp.Models;
using Xunit;

namespace weatherappTests
{
    public class WeatherRepositoryTests
    {
        private readonly IWeatherRepository weaterRepo;

        public WeatherRepositoryTests()
        {
            weaterRepo = new WeatherRepository();
        }
        [Fact]
        public void GetCountriesTests()
        {
            var c = weaterRepo.GetCountries();
            
            
            Assert.NotEmpty(c);
        }
        
        [Fact]
        public void GetCities()
        {
            var c = weaterRepo.GetCities("indonesia");

            Assert.NotEmpty(c);

        }
    }
}