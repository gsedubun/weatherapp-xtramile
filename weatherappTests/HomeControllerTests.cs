using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using weatherapp.Controllers;
using weatherapp.Models;
using Xunit;

namespace weatherappTests
{
    public class HomeControllerTests
    {
        private readonly Mock<IWeatherRepository> _weatherMock;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _weatherMock = new Mock<IWeatherRepository>();
            _controller = new HomeController(_weatherMock.Object);
            
        }
        [Fact]
        public void  countries_ActionExecutes_ReturnsOk()
        {
            _weatherMock.Setup(repo => repo.GetCountries())
                .Returns(new[] { "Indonesia", "Malaysia" });
            
            var result = _controller.countries();
         
            Assert.IsType<OkObjectResult>(result);
        }
        
        [Fact]
        public void  countries_ActionExecutes_ReturnsNoContent()
        {
            var result = _controller.countries();

            Assert.IsType<NoContentResult>(result);
        }
        
        [Fact]
        public void  cities_ActionExecutes_ReturnsOk()
        {
            _weatherMock.Setup(repo => repo.GetCities("Malaysia"))
                .Returns(new[] { "Kuala Lumpur", "Selangor" });
            
            var result = _controller.cities("Malaysia");
            
            _weatherMock.Verify(x=> x.GetCities("Malaysia"), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public void  cities_ActionExecutes_ReturnsNoContent()
        {
            var result = _controller.cities("Malaysia");

            _weatherMock.Verify(x=> x.GetCities("Malaysia"), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task   weather_ActionExecutes_ReturnsOk()
        { 
            _weatherMock.Setup(repo => repo.GetDataAsync("Malang"))
                .Returns( Task.FromResult( new WeatherData()
                {
                    Main = new Main(){ Temp = 73, FeelsLike = 79}
                }));
            
            var result = await _controller.weather("Malang");

            _weatherMock.Verify(x=>  x.GetDataAsync("Malang"), Times.Once);
            Assert.IsType<OkObjectResult>(result);
        }
        [Fact]
        public async Task   weather_ActionExecutes_ReturnsNoContent()
        {
            var result = await _controller.weather("asalaja");

            _weatherMock.Verify(x=>  x.GetDataAsync("asalaja"), Times.Once);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public  void   Fahrenheit_To_Celcius_Conversion()
        {
            var m = new Main();

            var result = m.ToCelcius(73);

            Assert.Equal(22.78, result);
        }

    }
}