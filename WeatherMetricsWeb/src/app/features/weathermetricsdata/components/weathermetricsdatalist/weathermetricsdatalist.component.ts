import { Component } from '@angular/core';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { WeatherMetricsLog } from '../../../../models/weathermetricslog.model';
import { WeathermetricsdataService } from '../../services/weathermetricsdata/weathermetricsdata.service';
import { HttpResponse } from "@angular/common/http";
import { MatDialog, MatDialogContent } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../../../shared/components/confirm-dialog/confirm-dialog.component';
import { Router } from '@angular/router';
import { DatePipe} from "@angular/common";

@Component({
  selector: 'app-weathermetricsdatalist',
  imports: [MatTableModule, MatIconModule, DatePipe],
  templateUrl: './weathermetricsdatalist.component.html',
  styleUrl: './weathermetricsdatalist.component.css',
  providers: [WeathermetricsdataService],
})
export class WeathermetricsdatalistComponent {

  displayedColumns: string[] = ['WeatherMetricsLogId', 'BarometricPressure', 'Humidity', 'TemperatureCelsius', 'WindSpeed', 'WindDirection', 'City', 'State', 'EntryDate', 'Actions'];

  private weathermetricsdataServ: WeathermetricsdataService;

  public weathermetricsData: WeatherMetricsLog[] = [];

  constructor(private weathermetricsdataService: WeathermetricsdataService, public dialog: MatDialog, private router: Router) {

    this.weathermetricsdataServ = weathermetricsdataService;

  }

  ngOnInit() {
    
    this.LoadWeatherMetricsData();

  }


  onAdd() {

      this.router.navigateByUrl("weathermetricslog/");

    };


private LoadWeatherMetricsData() {

this.weathermetricsdataServ.getWeatherMetricLogsAll()
      .subscribe(result => {
        if (result.statusCode == 200) {
          
          this.weathermetricsData = result.data!;

          //console.log(this.weathermetricsData);

        } else {

            console.log("Error Occured with HTTTP Status Code: #%d", result.statusCode);
            
        }
      });

}


  editRow(row: WeatherMetricsLog): void {

      this.router.navigateByUrl(`weathermetricslog/${row.weatherMetricsLogId}`);
      
  }




  deleteRow(row: WeatherMetricsLog): void {

    
       const dialogRef = this.dialog.open(ConfirmDialogComponent, {
          width: '300px',
          data: { message: 'Are you sure you want to proceed?' }
        });

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            // User confirmed, perform the action
            console.log('Delete Confirmed!');

            this.weathermetricsdataServ.deleteWeatherMetricLog(row.weatherMetricsLogId).subscribe(result => {
        if (result.statusCode == 200) {
          
          console.log("WeatherMetricsLog Deleted!");

          this.LoadWeatherMetricsData();


        } else {

            console.log("Error Occured with HTTTP Status Code: #%d", result.statusCode);
            
        }
      });

          } else {
            // User cancelled
            console.log('Delete Cancelled.');
          }
        });
  }

}
