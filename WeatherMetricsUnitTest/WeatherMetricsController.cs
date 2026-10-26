using AzureKeyVaultUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Moq;
using WeatherMetricsDTO.DTO;
using WeatherMetricsModel.Models;
using WeatherMetricsWebAPI.Controllers;
using WeatherMetricsWebAPI.ServicesImplementation;
using Microsoft.Extensions.Logging.Abstractions;


namespace WeatherMetricsUnitTest
{





    public class Startup
    {

        public class WeatherMetricsControllerTests
        {

            readonly IWeatherMetricsDataService? _weatherMetricsDataService;


            public WeatherMetricsControllerTests()
            {

            
            }


            [Fact]
            public void GetWeatherMetricsData_WeatherMetricsLogDTOList()
            {


                var mockService = new Mock<IWeatherMetricsDataService>();

                List<WeatherMetricsLogDTO> weatherMetricsLogDTOs = new List<WeatherMetricsLogDTO>() {

                new WeatherMetricsLogDTO
                {
                    WeatherMetricsLogId = 0,
                    BarometricPressure = 30,
                    Humidity = 20,
                    TemperatureCelsius = 15,
                    WindSpeed = 10,
                    WindDirection = "NE",
                    City = "Buffalo",
                    State = "NY"
                },
                new WeatherMetricsLogDTO
                {
                    WeatherMetricsLogId = 0,
                    BarometricPressure = 40,
                    Humidity = 25,
                    TemperatureCelsius = 12,
                    WindSpeed = 10,
                    WindDirection = "SE",
                    City = "East Aurora",
                    State = "NY"
                },

                new WeatherMetricsLogDTO
                {
                    WeatherMetricsLogId = 0,
                    BarometricPressure = 20,
                    Humidity = 18,
                    TemperatureCelsius = 16,
                    WindSpeed = 5,
                    WindDirection = "SW",
                    City = "Alden",
                    State = "NY"
                }

                };


                // Setup The Moq Service Call
                mockService.Setup(ms => ms.GetWeatherMetricsData()).Returns(weatherMetricsLogDTOs);

                var logger = NullLogger<WeatherMetricsController>.Instance;

                // Build configuration using a physical JSON file
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.test.json", optional: false, reloadOnChange: true)
                    .Build();


                // Inject the mock object (.Object) into the controller's constructor

                var controller = new WeatherMetricsController(logger, mockService.Object, configuration);

                List<WeatherMetricsLogDTO> weatherMetricsLogDTOResults = (List<WeatherMetricsLogDTO>)controller.GetWeatherMetricsAll();

                Assert.NotNull(weatherMetricsLogDTOResults);


            }

            
            

            
            

            

         



        }
    }
}
