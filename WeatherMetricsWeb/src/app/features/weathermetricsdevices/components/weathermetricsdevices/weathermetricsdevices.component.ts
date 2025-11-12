import { Component } from '@angular/core';
import { WeathermetricsdevicesService } from '../../services/weathermetricsdevices/weathermetricsdevices.service';

@Component({
  selector: 'app-weathermetricsdevices',
  imports: [],
  templateUrl: './weathermetricsdevices.component.html',
  styleUrl: './weathermetricsdevices.component.css',
  providers: [WeathermetricsdevicesService],
})
export class WeathermetricsdevicesComponent {

private weathermetricsdevicesServ : WeathermetricsdevicesService;
isEnabled? : boolean;
loggingInterval : number = 0;
strIoTDeviceID? : string = "";
strIoTDeviceUptime? : string;
strIoTDeviceStatus? : string;


constructor(private weathermetricsdevicesService : WeathermetricsdevicesService ) {

  this.weathermetricsdevicesServ = weathermetricsdevicesService;

}


 ngOnInit() {

    // Populate IoT Device Selection From IoT Hub

    this.strIoTDeviceID = ""; 

    // weathermetricsdevicesService
    

  }

  onEnable() {

    console.log("Weather Metrics Device Enable");

    this.isEnabled = true;

  }

  onDisable() {

    console.log("Weather Metrics Device Disable");

    this.isEnabled = false;

  }

  onDeviceSelectionChange(event: Event) {

    console.log("Weather Metrics Device Selection Change");


    this.strIoTDeviceID = (event.target as HTMLInputElement).value;

    // weathermetricsdevicesService

    // Query Device Status

    // Update UI For Device Status

  }

  onLoggingIntervalSelectionChange(event: Event) {

    console.log("Device Logging Interval Change");

    if ( (event.target as HTMLInputElement).value != "") {

      this.loggingInterval = Number((event.target as HTMLInputElement).value);
      
    }

  }


  onDeviceUpdate(){

    console.log("Device Update");

    // Update Device Logging Interval, & Enable Disable Status

    // weathermetricsdevicesService


  }

}
