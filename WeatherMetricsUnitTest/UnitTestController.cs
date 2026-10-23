using AzureKeyVaultUtilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WeatherMetricsDTO.DTO;
using WeatherMetricsModel.Models;
using WeatherMetricsWebAPI.Controllers;
using WeatherMetricsWebAPI.ServicesImplementation;


namespace WeatherMetricsUnitTest
{





    public class Startup
    {

        public class UnitTestController
        {

            readonly IWeatherMetricsDataService? _weatherMetricsDataService;


            public UnitTestController()
            {

                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration configuration = builder.Build();

                //string? dBConnectionString = configuration.GetConnectionString("DBConnection");

                string? dBConnectionString = Task.Run(async () => await KeyVaultUtil.GetKeyVaultDatabaseConnection(configuration)).GetAwaiter().GetResult();




                this._weatherMetricsDataService = new WeatherMetricsDataService(dBConnectionString);

            }


            [Fact]
            public void GetWeatherMetricsData_WeatherMetricsLogDTOList()
            {


                IEnumerable<WeatherMetricsLogDTO> weatherMetricsLogDTOs = _weatherMetricsDataService.GetWeatherMetricsData();


                Assert.NotNull(weatherMetricsLogDTOs);




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
}
