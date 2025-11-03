using System;
using System.Collections.Generic;

namespace WeatherMetricsModel.Models;

public partial class WeatherMetricsLog
{
    public long WeatherMetricsLogId { get; set; }

    public int? BarometricPressure { get; set; }

    public int? Humidity { get; set; }

    public int? TemperatureCelsius { get; set; }

    public int? WindSpeed { get; set; }

    public string? WindDirection { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public DateTime? EntryDate { get; set; }
}
