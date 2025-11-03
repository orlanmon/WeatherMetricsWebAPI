export class WeatherMetricsLog {


  constructor(weatherMetricsLogId: number, barometricPressure: number, humidity: number, temperatureCelsius: number, windSpeed: number, windDirection: string, city: string, state: string, entryDate: Date) {
    this.weatherMetricsLogId = weatherMetricsLogId;
    this.barometricPressure = barometricPressure;
    this.humidity = humidity;
    this.temperatureCelsius = temperatureCelsius;
    this.windSpeed = windSpeed;
    this.windDirection = windDirection;
    this.city = city;
    this.state = state;
    this.entryDate = entryDate;

  }

  weatherMetricsLogId : number;

  barometricPressure: number;

  humidity: number;

  temperatureCelsius: number;

  windSpeed: number;

  windDirection: string;

  entryDate: Date;

  city: string;

  state: string;

}

