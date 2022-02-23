import { Component, OnInit } from '@angular/core';
import { MongoDbService } from "../mongoDbService/mongoDbService";
import {DataService} from "../data.service";

import { TickerModel } from "../mongoDbService/tickerModel";
import {Options} from "highcharts/highstock";

@Component({
  selector: 'app-ticker',
  templateUrl: './ticker.component.html',
  styleUrls: ['./ticker.component.css']
})

export class TickerComponent implements OnInit {

  manufacturer: string = "Sony";

  chartData: Array<number[]> = [[1546441200000, 1045], [1546527600000, 1016]];

  loading: boolean = false;
  errorMessage: string = "";

  constructor(private mongoService: MongoDbService, private dataService: DataService) {
  }

  ngOnInit() {
    this.dataService.getData().subscribe(result => {
      this.chartData = result.map(x => [new Date(x.date).getTime(), Number.parseFloat(x.value)])
      this.chartOptions = {
        series: [
          {
            type: "line",
            id: "base",
            pointInterval: 24 * 3600 * 1000,
            data: this.chartData
          },
          {
            type: "zigzag",
            showInLegend: true,
            linkedTo: "base"
          }
        ]
      };
    })
  }

  chartOptions: Options = {
    series: [
      {
        type: "line",
        id: "base",
        pointInterval: 24 * 3600 * 1000,
        data: this.chartData
      },
      {
        type: "zigzag",
        showInLegend: true,
        linkedTo: "base"
      }
    ]
  };

  // public getTickers() {
  //   this.loading = true;
  //   this.errorMessage = "";
  //   this.mongoService.callMongoApi(this.manufacturer)
  //     .subscribe(
  //       (response) => {                           //next() callback
  //         console.log('response received')
  //         this.tickers = response;
  //         console.log(this.tickers);
  //       },
  //       (error) => {                              //error() callback
  //         console.error('Request failed with error')
  //         this.errorMessage = error;
  //         this.loading = false;
  //       },
  //       () => {                                   //complete() callback
  //         console.error('Request completed')      //This is actually not needed
  //         this.loading = false;
  //       })
  // }

}
