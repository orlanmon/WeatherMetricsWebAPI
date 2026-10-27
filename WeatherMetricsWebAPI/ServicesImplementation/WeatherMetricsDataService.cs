using WeatherMetricsDTO.DTO;
using WeatherMetricsModel.Models;
using System.Linq;
using WeatherMetricsDTO;
using Microsoft.EntityFrameworkCore;

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

        public async Task<Int64> InsertWeatherMetricsData(WeatherMetricsLogDTO weatherMetricsLogDTO)
        {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                WeatherMetricsLog? weatherMetricsLog;


                if (weatherMetricsLogDTO == null)
                {
                 
                        throw new ArgumentNullException(nameof(weatherMetricsLogDTO));

                }

                weatherMetricsLog = _objAutoMapper.Map<WeatherMetricsLogDTO, WeatherMetricsLog>(weatherMetricsLogDTO);


                await weatherMetricsDbContext.WeatherMetricsLogs.AddAsync(weatherMetricsLog);


                await weatherMetricsDbContext.SaveChangesAsync();


                return weatherMetricsLog.WeatherMetricsLogId;
            }
        }

        public async Task UpdateWeatherMetricsData(WeatherMetricsLogDTO weatherMetricsLogDTO) {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                WeatherMetricsLog? weatherMetricsLog;
                WeatherMetricsLog? weatherMetricsLogToUpdate;


                if (weatherMetricsLogDTO == null)
                {

                    throw new ArgumentNullException(nameof(weatherMetricsLogDTO));

                }

                weatherMetricsLog = _objAutoMapper.Map<WeatherMetricsLogDTO, WeatherMetricsLog>(weatherMetricsLogDTO);

                weatherMetricsLogToUpdate = await weatherMetricsDbContext.WeatherMetricsLogs.FirstOrDefaultAsync(weatherMetricsLog => weatherMetricsLog.WeatherMetricsLogId == weatherMetricsLogDTO.WeatherMetricsLogId);


                if(weatherMetricsLogToUpdate != null)
                {

                    // Perform Update Using Automapper 
                    _objAutoMapper.Map(weatherMetricsLogDTO, weatherMetricsLogToUpdate);

                    await weatherMetricsDbContext.SaveChangesAsync();
                }

            }
        }

        public async Task DeleteWeatherMetricsData(Int64 weatherMetricsLogID) {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                WeatherMetricsLog? weatherMetricsLogToDelete;
                
                weatherMetricsLogToDelete = await weatherMetricsDbContext.WeatherMetricsLogs.FirstOrDefaultAsync(weatherMetricsLog => weatherMetricsLog.WeatherMetricsLogId == weatherMetricsLogID);


                if (weatherMetricsLogToDelete != null)
                {

                    weatherMetricsDbContext.WeatherMetricsLogs.Remove(weatherMetricsLogToDelete);

                    await weatherMetricsDbContext.SaveChangesAsync();

                }

            }

        }

        public async  Task<WeatherMetricsLogDTO?> GetWeatherMetricsData(Int64 weatherMetricsLogID) {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                WeatherMetricsLogDTO? weatherMetricsLogDTOReturn = null;

                WeatherMetricsLog? weatherMetricsLogReturn;

                weatherMetricsLogReturn = await weatherMetricsDbContext.WeatherMetricsLogs.FirstOrDefaultAsync(weatherMetricsLog => weatherMetricsLog.WeatherMetricsLogId == weatherMetricsLogID);

                if (weatherMetricsLogReturn != null)
                {
                    weatherMetricsLogDTOReturn = _objAutoMapper.Map<WeatherMetricsLog, WeatherMetricsLogDTO>(weatherMetricsLogReturn);
                }

                return weatherMetricsLogDTOReturn;

            }

        }

        public async Task<IEnumerable<WeatherMetricsLogDTO>> GetWeatherMetricsData(DateTime startDate, DateTime endDate) {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                List<WeatherMetricsLogDTO> weatherMetricsLogDTOs = new List<WeatherMetricsLogDTO>();
                //IQueryable<WeatherMetricsLog>?weatherMetricsLogReturn = null;
                List< WeatherMetricsLog >? weatherMetricsLogReturn = null;

                // Note: Where Clause Does not actually execute it only modifies the  I/O query expression Tree
                // That will be used to create the Underlying SQL 
                // [ LINQ Query ] ---> [ Expression Tree ] ---> [ EF Core Provider ] ---> [ SQL Generated ]
                // await only happens on the ToListAsync when the results are converted back to a generic collection
                weatherMetricsLogReturn = await weatherMetricsDbContext.WeatherMetricsLogs.Where(weatherMetricsLog => (weatherMetricsLog.EntryDate >= startDate && weatherMetricsLog.EntryDate <= endDate)).ToListAsync<WeatherMetricsLog>();

                if (weatherMetricsLogReturn != null) {

                    foreach (WeatherMetricsLog weatherMetricsLog in weatherMetricsLogReturn)
                    {

                        weatherMetricsLogDTOs.Add(_objAutoMapper.Map<WeatherMetricsLog, WeatherMetricsLogDTO>(weatherMetricsLog));

                    }

                }

                return weatherMetricsLogDTOs;
            }

        }

        public async Task<IEnumerable<WeatherMetricsLogDTO>> GetWeatherMetricsData()
        {

            using (WeatherMetricsDbContext weatherMetricsDbContext = new WeatherMetricsDbContext(_dBConnectionString))
            {

                List<WeatherMetricsLogDTO> weatherMetricsLogDTOs = new List<WeatherMetricsLogDTO>();
                //IQueryable<WeatherMetricsLog>? weatherMetricsLogReturn = null;
                List<WeatherMetricsLog>? weatherMetricsLogReturn = null;


                weatherMetricsLogReturn = await weatherMetricsDbContext.WeatherMetricsLogs.OrderByDescending(wml => wml.WeatherMetricsLogId).ToListAsync<WeatherMetricsLog>();


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
