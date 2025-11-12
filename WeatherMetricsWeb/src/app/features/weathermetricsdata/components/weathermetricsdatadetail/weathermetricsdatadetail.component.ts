import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { WeathermetricsdataService } from '../../services/weathermetricsdata/weathermetricsdata.service';
import { ActivatedRoute } from '@angular/router';
import { WeatherMetricsLog} from '../../../../models/weathermetricslog.model';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { MatDialog, MatDialogContent } from '@angular/material/dialog';


@Component({
  selector: 'app-weathermetricsdatadetail',
  imports: [ReactiveFormsModule, CommonModule],
  templateUrl: './weathermetricsdatadetail.component.html',
  styleUrl: './weathermetricsdatadetail.component.css',
  providers: [WeathermetricsdataService],
})
export class WeathermetricsdatadetailComponent {

   isAddMode? : boolean
   webForm!: FormGroup;
   weatherMetricsLog! : WeatherMetricsLog;
   strDialogName? : string;



   constructor(private route: ActivatedRoute, private weathermetricsdataServ: WeathermetricsdataService, private router: Router, public dialog: MatDialog) {

   }

    onSubmit(){

      if(this.isAddMode) {

        this.weatherMetricsLog = new WeatherMetricsLog(0,0,0,0,0,'', '', '', new Date());

        // Pull Form Data into Model
        this.weatherMetricsLog.barometricPressure = this.webForm.get('barometricPressure')?.value;
        this.weatherMetricsLog.humidity = this.webForm.get('humidity')?.value;
        this.weatherMetricsLog.temperatureCelsius = this.webForm.get('temperatureCelsius')?.value;
        this.weatherMetricsLog.windDirection = this.webForm.get('windDirection')?.value;
        this.weatherMetricsLog.windSpeed = this.webForm.get('windSpeed')?.value;
        this.weatherMetricsLog.city = this.webForm.get('city')?.value;
        this.weatherMetricsLog.state = this.webForm.get('state')?.value;

        this.weathermetricsdataServ.insertWeatherMetricLog(this.weatherMetricsLog).subscribe(result => {
        if (result.statusCode == 200) {
          
          console.log("WeatherMetricsLog Added! WeatherMetricsLogID = #%d", result.data);

          this.router.navigateByUrl("weathermetricsdata");

        } else {

            console.log("Error Occured with HTTTP Status Code: #%d", result.statusCode);
            


        }
      });

      } else {

        // WeatherMetricsLog Is Already Initialized
        this.weatherMetricsLog.barometricPressure = this.webForm.get('barometricPressure')?.value;
        this.weatherMetricsLog.humidity = this.webForm.get('humidity')?.value;
        this.weatherMetricsLog.temperatureCelsius = this.webForm.get('temperatureCelsius')?.value;
        this.weatherMetricsLog.windDirection = this.webForm.get('windDirection')?.value;
        this.weatherMetricsLog.windSpeed = this.webForm.get('windSpeed')?.value;
        this.weatherMetricsLog.city = this.webForm.get('city')?.value;
        this.weatherMetricsLog.state = this.webForm.get('state')?.value;  

          this.weathermetricsdataServ.updateWeatherMetricLog(this.weatherMetricsLog).subscribe(result => {
        if (result.statusCode == 200) {
          
          console.log("WeatherMetricsLog Updated!");

          this.router.navigateByUrl("weathermetricsdata");

        } else {

            console.log("Error Occured with HTTTP Status Code: #%d", result.statusCode);
            
        }
      });
      }

    };


    onReturn() {

      this.router.navigateByUrl("weathermetricsdata");

    };


   ngOnInit() {
    
    const id = this.route.snapshot.params['id']; // Get ID from route for edit mode
    
    if( id.length === 0 ) {
      this.isAddMode = true;
      this.strDialogName = "Add Weather Metric Log";
    } else {
      this.isAddMode = false;
      this.strDialogName = "Edit Weather Metric Log";
    }

    this.webForm = new FormGroup(
      { barometricPressure: new FormControl(Number, Validators.required),
        humidity: new FormControl(Number, Validators.required),
        temperatureCelsius: new FormControl(Number,Validators.required),
        windSpeed: new FormControl(Number,Validators.required),
        windDirection: new FormControl('', Validators.required),
        city: new FormControl('', Validators.required),
        state: new FormControl('', Validators.required),
      }
    );
        
    if( this.isAddMode == false ) {

      // Populate Form Here

      this.weathermetricsdataServ.getWeatherMetricLog(id).subscribe(result => {

        if (result.statusCode == 200) {
          

          if( result.data ) {
              this.weatherMetricsLog = result.data;

              const formData = {

                barometricPressure: this.weatherMetricsLog.barometricPressure,                
                humidity: this.weatherMetricsLog.humidity,
                temperatureCelsius: this.weatherMetricsLog.temperatureCelsius,
                windSpeed: this.weatherMetricsLog.windSpeed,
                windDirection: this.weatherMetricsLog.windDirection,
                city: this.weatherMetricsLog.city,
                state: this.weatherMetricsLog.state

              }

              this.webForm.setValue(formData);

          }
          
          console.log("WeatherMetricsLog Data Loaded");

        } else {

            console.log("Error Occured with HTTTP Status Code: #%d", result.statusCode);
            
        }

    });

    }

  }
}

