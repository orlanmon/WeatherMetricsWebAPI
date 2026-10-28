using AzureKeyVaultUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using WeatherMetricsDTO.DTO;
using WeatherMetricsModel.Models;
using WeatherMetricsWebAPI.Controllers;
using WeatherMetricsWebAPI.ServicesImplementation;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;


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
            public async Task GetWeatherMetricsData_WeatherMetricsLogDTOList()
            {


                // Instance of Mock Service - Mock of IWeatherMetricsDataService
                var mockService = new Mock<IWeatherMetricsDataService>();

                // Setup Mock Data for Mock Service Method to return
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


                // Setup Behavior of Moq Service Method - IWeatherMetricsDataService::GetWeatherMetricsData 

                //mockService.Setup(ms => ms.GetWeatherMetricsData()).Returns<WeatherMetricsLogDTO>(weatherMetricsLogDTOs);
                // mockService.Setup(ms => ms.GetWeatherMetricsData()).Returns(Task.FromResult<IEnumerable<WeatherMetricsLogDTO>>(weatherMetricsLogDTOs));

                mockService.Setup(ms => ms.GetWeatherMetricsData()).Returns(Task.FromResult((IEnumerable<WeatherMetricsLogDTO>)weatherMetricsLogDTOs));


                var logger = NullLogger<WeatherMetricsController>.Instance;

          
                // 1. Create the mock configuration
                var mockConfig = new Mock<IConfiguration>();

                // 2. Setup the specific configuration key route Mock
                mockConfig.Setup(c => c["AppSettings:WebAPIVersion"]).Returns("2.0");


                // Inject the mock object (.Object) into the controller's constructor
                // This Replaces The Default Dependecy Injected WeatherMetricsDataService with Mock Service

                var controller = new WeatherMetricsController(logger, mockService.Object, mockConfig.Object);


                // Now Call Controller Method which calls GetWeatherMetricsData but now it's the Mock Version
                ActionResult<IEnumerable<WeatherMetricsLogDTO>> actionResult = await controller.GetWeatherMetricsAll();

               
                var actionResuktOKResult = actionResult.Result as Microsoft.AspNetCore.Mvc.OkObjectResult;

                List<WeatherMetricsLogDTO>? weatherMetricsLogDTOResults = null;

                if (actionResuktOKResult != null && actionResuktOKResult.Value != null ) { 

                    weatherMetricsLogDTOResults = actionResuktOKResult.Value as List<WeatherMetricsLogDTO>;

                }

                Assert.NotNull(weatherMetricsLogDTOResults);


            }
        }
    }
}
