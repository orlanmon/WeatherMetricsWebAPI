using System.Collections.Generic;
using WeatherMetricsDTO.DTO;

namespace WeatherMetricsWebAPI.ServicesImplementation
{
    public interface IWeatherMetricsDataService
    {
        // Note: he async keyword is an implementation detail that tells the compiler to generate a state machine; it belongs exclusively in the concrete implementation.

        public Task<Int64> InsertWeatherMetricsData(WeatherMetricsLogDTO weatherMetricsLogDTO);
        public Task UpdateWeatherMetricsData(WeatherMetricsLogDTO weatherMetricsLogDTO);
        public Task DeleteWeatherMetricsData(Int64 weatherMetricsLogID);
        public Task<WeatherMetricsLogDTO?> GetWeatherMetricsData(Int64 weatherMetricsLogID);
        public Task<IEnumerable<WeatherMetricsLogDTO>> GetWeatherMetricsData(DateTime startDate, DateTime endDate );
        public Task<IEnumerable<WeatherMetricsLogDTO>> GetWeatherMetricsData();

    }
}
