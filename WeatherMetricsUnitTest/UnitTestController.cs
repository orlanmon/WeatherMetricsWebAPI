using Moq;
using WeatherMetricsWebAPI.Controllers;
using WeatherMetricsWebAPI.ServicesImplementation;
using WeatherMetricsDTO.DTO;
using WeatherMetricsModel.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;


namespace WeatherMetricsUnitTest

{
    public class UnitTestController
    {

        private readonly Mock<IWeatherMetricsDataService> _weatherMetricsDataService;

        public UnitTestController() {


            _weatherMetricsDataService = new Mock<IWeatherMetricsDataService>();





        }


        [Fact]
        public void GetWeatherMetricsData_WeatherMetricsLogDTOList()
        {

            List<WeatherMetricsLogDTO> weatherMetricsLogDTOs = GetWeatherMetricsLogDTOData();




            _weatherMetricsDataService.Setup( x => x.GetWeatherMetricsData()).Returns(weatherMetricsLogDTOs);


            var mockLogger = new Mock<ILogger<WeatherMetricsController>>();


            // Inject Mock IWeatherMetricsDataService Service Into Controller
            var weatherMetricsController = new WeatherMetricsController(mockLogger.Object, _weatherMetricsDataService.Object);

           
            IActionResult weatherMetricsDataServiceResult = weatherMetricsController.GetWeatherMetricsAll();
            
            OkObjectResult? okResult = weatherMetricsDataServiceResult as OkObjectResult;

            List<WeatherMetricsLogDTO>? weatherMetricsDTOResultArray = okResult.Value as List<WeatherMetricsLogDTO>;




            Assert.NotNull(weatherMetricsDTOResultArray);
            Assert.Equal(GetWeatherMetricsLogDTOData().Count(), weatherMetricsDTOResultArray.Count());




        }

        public void GetWeatherMetricsDataByID_WeatherMetricsLogDTO()
        {


        }


        private List<WeatherMetricsLogDTO> GetWeatherMetricsLogDTOData()
        {

            List<WeatherMetricsLogDTO> weatherMetricsLogDTOs = new List<WeatherMetricsLogDTO>()
            {

                new WeatherMetricsLogDTO { WeatherMetricsLogId = 0,
                                           BarometricPressure = 30,
                                           Humidity = 20,
                                           TemperatureCelsius = 15,
                                           WindSpeed = 10,
                                           WindDirection = "NE",
                                           City = "Buffalo",
                                           State = "NY"
                },
                new WeatherMetricsLogDTO { WeatherMetricsLogId = 0,
                                           BarometricPressure = 40,
                                           Humidity = 25,
                                           TemperatureCelsius = 12,
                                           WindSpeed = 10,
                                           WindDirection = "SE",
                                           City = "East Aurora",
                                           State = "NY"
                },

                new WeatherMetricsLogDTO { WeatherMetricsLogId = 0,
                                           BarometricPressure = 20,
                                           Humidity = 18,
                                           TemperatureCelsius = 16,
                                           WindSpeed = 5,
                                           WindDirection = "SW",
                                           City = "Alden",
                                           State = "NY"
                }
            };

            return weatherMetricsLogDTOs;

        }



    }
}
