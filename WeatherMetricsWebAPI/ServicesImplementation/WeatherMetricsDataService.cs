using WeatherMetricsDTO.DTO;
using WeatherMetricsModel.Models;
using System.Linq;
using WeatherMetricsDTO;

namespace WeatherMetricsWebAPI.ServicesImplementation
{
    public class WeatherMetricsDataService : IWeatherMetricsDataService
    {

        private readonly string? _dBConnectionString;
        private readonly AutoMapper.IMapper? _objAutoMapper;


        public WeatherMetricsDataService(string dBConnectionString) {

            _dBConnectionString = dBConnectionString;


            // Configure AutoMapper For Model to DTOs 
            AutoMapperConfiguration.Configure();


            _objAutoMapper = AutoMapperConfiguration.AutoMapperConfig.CreateMapper();

        }

        public Int64 InsertWeatherMetricsData(WeatherMetricsLogDTO weatherMetricsLogDTO)
        {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                WeatherMetricsLog? weatherMetricsLog;


                if (weatherMetricsLogDTO == null)
                {
                 
                        throw new ArgumentNullException(nameof(weatherMetricsLogDTO));

                }

                weatherMetricsLog = _objAutoMapper.Map<WeatherMetricsLogDTO, WeatherMetricsLog>(weatherMetricsLogDTO);


                weatherMetricsDbContext.WeatherMetricsLogs.Add(weatherMetricsLog);

                weatherMetricsDbContext.SaveChanges();

                return weatherMetricsLog.WeatherMetricsLogId;
            }
        }

        public void UpdateWeatherMetricsData(WeatherMetricsLogDTO weatherMetricsLogDTO) {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                WeatherMetricsLog? weatherMetricsLog;
                WeatherMetricsLog? weatherMetricsLogToUpdate;


                if (weatherMetricsLogDTO == null)
                {

                    throw new ArgumentNullException(nameof(weatherMetricsLogDTO));

                }

                weatherMetricsLog = _objAutoMapper.Map<WeatherMetricsLogDTO, WeatherMetricsLog>(weatherMetricsLogDTO);

                weatherMetricsLogToUpdate = weatherMetricsDbContext.WeatherMetricsLogs.FirstOrDefault(weatherMetricsLog => weatherMetricsLog.WeatherMetricsLogId == weatherMetricsLogDTO.WeatherMetricsLogId);


                if(weatherMetricsLogToUpdate != null)
                {

                    // Perform Update Using Automapper 
                    _objAutoMapper.Map(weatherMetricsLogDTO, weatherMetricsLogToUpdate);

                    weatherMetricsDbContext.SaveChanges();
                }

            }
        }

        public void DeleteWeatherMetricsData(Int64 weatherMetricsLogID) {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                WeatherMetricsLog? weatherMetricsLogToDelete;
                
                weatherMetricsLogToDelete = weatherMetricsDbContext.WeatherMetricsLogs.FirstOrDefault(weatherMetricsLog => weatherMetricsLog.WeatherMetricsLogId == weatherMetricsLogID);


                if (weatherMetricsLogToDelete != null)
                {

                    weatherMetricsDbContext.WeatherMetricsLogs.Remove  (weatherMetricsLogToDelete);

                    weatherMetricsDbContext.SaveChanges();
                }

            }

        }

        public WeatherMetricsLogDTO GetWeatherMetricsData(Int64 weatherMetricsLogID) {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                WeatherMetricsLogDTO? weatherMetricsLogDTOReturn = null;

                WeatherMetricsLog weatherMetricsLogReturn;

                weatherMetricsLogReturn = weatherMetricsDbContext.WeatherMetricsLogs.FirstOrDefault(weatherMetricsLog => weatherMetricsLog.WeatherMetricsLogId == weatherMetricsLogID);

                if (weatherMetricsLogReturn != null)
                {
                    weatherMetricsLogDTOReturn = _objAutoMapper.Map<WeatherMetricsLog, WeatherMetricsLogDTO>(weatherMetricsLogReturn);
                }

                return weatherMetricsLogDTOReturn;

            }

        }

        public IEnumerable<WeatherMetricsLogDTO> GetWeatherMetricsData(DateTime startDate, DateTime endDate) {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                List<WeatherMetricsLogDTO> weatherMetricsLogDTOs = new List<WeatherMetricsLogDTO>();
                IQueryable<WeatherMetricsLog>?weatherMetricsLogReturn = null;



                weatherMetricsLogReturn = weatherMetricsDbContext.WeatherMetricsLogs.Where(weatherMetricsLog => (weatherMetricsLog.EntryDate >= startDate && weatherMetricsLog.EntryDate <= endDate) );

                if (weatherMetricsLogReturn != null) {


                    foreach (WeatherMetricsLog weatherMetricsLog in weatherMetricsLogReturn)
                    {

                        weatherMetricsLogDTOs.Add(_objAutoMapper.Map<WeatherMetricsLog, WeatherMetricsLogDTO>(weatherMetricsLog));


                    }

                }

                return weatherMetricsLogDTOs;
            }

        }


        public IEnumerable<WeatherMetricsLogDTO> GetWeatherMetricsData()
        {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                List<WeatherMetricsLogDTO> weatherMetricsLogDTOs = new List<WeatherMetricsLogDTO>();
                IQueryable<WeatherMetricsLog>? weatherMetricsLogReturn = null;


                weatherMetricsLogReturn = weatherMetricsDbContext.WeatherMetricsLogs.OrderByDescending(wml => wml.WeatherMetricsLogId); 

                if (weatherMetricsLogReturn != null)
                {


                    foreach (WeatherMetricsLog weatherMetricsLog in weatherMetricsLogReturn)
                    {

                        weatherMetricsLogDTOs.Add(_objAutoMapper.Map<WeatherMetricsLog, WeatherMetricsLogDTO>(weatherMetricsLog));


                    }

                }

                return weatherMetricsLogDTOs;
            }

        }

    }
}
