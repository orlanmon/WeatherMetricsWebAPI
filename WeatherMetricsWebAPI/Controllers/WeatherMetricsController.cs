using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WeatherMetricsDTO.DTO;
using WeatherMetricsModel.Models;
using WeatherMetricsWebAPI.ServicesImplementation;


namespace WeatherMetricsWebAPI.Controllers
{
    [ApiController]
    //[Route("api/[controller]/[action]")]
    public class WeatherMetricsController : ControllerBase
    {

        private readonly ILogger<WeatherMetricsController> _logger;
        private readonly IWeatherMetricsDataService _weatherMetricsDataService;
        private readonly IConfiguration _configuration;


        public WeatherMetricsController(ILogger<WeatherMetricsController> logger, IWeatherMetricsDataService weatherMetricsDataService, IConfiguration configuration)
        {
            _logger = logger;
            
            _weatherMetricsDataService = weatherMetricsDataService;

            _configuration = configuration;


        }




        [ActionName("Insert")]
        [HttpPost("api/[controller]/[action]")]
        public IActionResult InsertWeatherMetricsData([FromBody]  WeatherMetricsLogDTO weatherMetricsLogDTO) {

            Int64? weatherMetricsLogID = null;

            try
            {

                weatherMetricsLogID = _weatherMetricsDataService.InsertWeatherMetricsData(weatherMetricsLogDTO);

                _logger.LogInformation("WeatherMetricsController::InsertWeatherMetricsData Success");


                return Ok(weatherMetricsLogID);

            }
            catch (Exception ex) {

                _logger.LogError("WeatherMetricsController::InsertWeatherMetricsData Failed: {0}", ex.Message );


                return StatusCode(500, "An unexpected server error occurred. " + ex.Message);

            }
            finally
            {
                

            }
        }

        [ActionName("Update")]
        [HttpPost("api/[controller]/[action]")]
        public IActionResult UpdateWeatherMetricsData([FromBody]  WeatherMetricsLogDTO weatherMetricsLogDTO) {

          
            try
            {

                _weatherMetricsDataService.UpdateWeatherMetricsData(weatherMetricsLogDTO);


                _logger.LogInformation("WeatherMetricsController::UpdateWeatherMetricsData Success");


                return Ok();

            }
            catch (Exception ex)
            {

                _logger.LogError("WeatherMetricsController::UpdateWeatherMetricsData Failed: {0}", ex.Message);


                return StatusCode(500, "An unexpected server error occurred. " + ex.Message);

            }
            finally
            {

               

            }


        }

        [ActionName("Delete")]
        [HttpDelete("api/[controller]/[action]/{weatherMetricsLogID}")]
        public IActionResult Delete(Int64 weatherMetricsLogID) {

           
            try
            {

                _weatherMetricsDataService.DeleteWeatherMetricsData(weatherMetricsLogID);

                _logger.LogInformation("WeatherMetricsController::Delete Success");

                return Ok();

            }
            catch (Exception ex)
            {


                _logger.LogError("WeatherMetricsController::Delete Failed: {0}", ex.Message);

                return StatusCode(500, "An unexpected server error occurred. " + ex.Message);

            }
            finally
            {
               

            }


        }


        [ActionName("Get")]
        [HttpGet("api/[controller]/[action]/{weatherMetricsLogID}")]
        public IActionResult GetWeatherMetricsData(Int64 weatherMetricsLogID) {

            WeatherMetricsLogDTO? weatherMetricsLogDTO = null;
           
           

            try
            {
               

                weatherMetricsLogDTO = _weatherMetricsDataService.GetWeatherMetricsData(weatherMetricsLogID);


                _logger.LogInformation("WeatherMetricsController::GetWeatherMetricsData Success");


                return Ok(weatherMetricsLogDTO);

            }
            catch (Exception ex)
            {

                _logger.LogError("WeatherMetricsController::GetWeatherMetricsData Failed: {0}", ex.Message);

                return StatusCode(500, "An unexpected server error occurred. " + ex.Message);

            }
            finally
            {

               


            }
    
 
        }

        [ActionName("Get")]
        [HttpGet("api/[controller]/[action]/{startDate}/{endDate}")]
        public IActionResult GetWeatherMetricsDataRange(DateTime startDate, DateTime endDate) {

            IEnumerable<WeatherMetricsLogDTO>? weatherMetricsLogDTOs = null;

            try
            {
                
                weatherMetricsLogDTOs = _weatherMetricsDataService.GetWeatherMetricsData(startDate, endDate);

                _logger.LogInformation("WeatherMetricsController::GetWeatherMetricsDataRange Success");



                return Ok(weatherMetricsLogDTOs);

            }
            catch (Exception ex)
            {

                _logger.LogError("WeatherMetricsController::GetWeatherMetricsDataRange Failed: {0}", ex.Message);


                return StatusCode(500, "An unexpected server error occurred. " + ex.Message);

            }
            finally
            {
              
            }
       
        }



        [ActionName("Get")]
        [HttpGet("api/[controller]/[action]")]
        public IActionResult GetWeatherMetricsAll()
        {

            IEnumerable<WeatherMetricsLogDTO>? weatherMetricsLogDTOs = null;

            try
            {

                weatherMetricsLogDTOs = _weatherMetricsDataService.GetWeatherMetricsData();


                _logger.LogInformation("WeatherMetricsController::GetWeatherMetricsAll Success");



                return Ok(weatherMetricsLogDTOs);

            }
            catch (Exception ex)
            {

                _logger.LogError("WeatherMetricsController::GetWeatherMetricsAll Failed: {0}", ex.Message);


                return StatusCode(500, "An unexpected server error occurred. " + ex.Message);

            }
            finally
            {

            }

        }


        [ActionName("GetAPIVersion")]
        [HttpGet("api/[controller]/[action]")]
        public IActionResult GetAPIVersion()
        {
            try
            {

                float webAPIVersion =  ( _configuration["AppSettings:WebAPIVersion"] != null ?  float.Parse(_configuration["AppSettings:WebAPIVersion"]) : 1.0f );


                return Ok(webAPIVersion);

            }
            catch (Exception ex)
            {

                _logger.LogError("WeatherMetricsController::GetAPIVersion Failed: {0}", ex.Message);


                return StatusCode(500, "An unexpected server error occurred. " + ex.Message);

            }
            finally
            {

            }

        }




    }
}
