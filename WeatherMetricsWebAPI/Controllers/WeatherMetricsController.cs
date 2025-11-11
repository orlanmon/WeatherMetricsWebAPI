using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IWeatherMetricsDataService _WeatherMetricsDataService;
        private readonly IWeatherMetricsDataService _weatherMetricsDataService;


        public WeatherMetricsController(ILogger<WeatherMetricsController> logger, IWeatherMetricsDataService weatherMetricsDataService)
        {
            _logger = logger;
            _WeatherMetricsDataService = weatherMetricsDataService;
            
            _weatherMetricsDataService = weatherMetricsDataService;

        }

        [ActionName("Insert")]
        [HttpPost("api/[controller]/[action]")]
        public IActionResult InsertWeatherMetricsData([FromBody]  WeatherMetricsLogDTO weatherMetricsLogDTO) {

            Int64? weatherMetricsLogID = null;

            try
            {

                weatherMetricsLogID = _weatherMetricsDataService.InsertWeatherMetricsData(weatherMetricsLogDTO);

                return Ok(weatherMetricsLogID);

            }
            catch (Exception ex) {

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


                return Ok();

            }
            catch (Exception ex)
            {

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


                return Ok();

            }
            catch (Exception ex)
            {

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


                return Ok(weatherMetricsLogDTO);

            }
            catch (Exception ex)
            {

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


                return Ok(weatherMetricsLogDTOs);

            }
            catch (Exception ex)
            {

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


                return Ok(weatherMetricsLogDTOs);

            }
            catch (Exception ex)
            {

                return StatusCode(500, "An unexpected server error occurred. " + ex.Message);

            }
            finally
            {

            }

        }

    }
}
