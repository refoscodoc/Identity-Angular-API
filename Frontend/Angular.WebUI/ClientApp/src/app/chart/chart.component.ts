import { Component, OnInit} from "@angular/core";
import * as Highcharts from "highcharts/highstock";
import { Options } from "highcharts/highstock";

import IndicatorsCore from "highcharts/indicators/indicators";
import IndicatorZigzag from "highcharts/indicators/zigzag";
import {DataService} from "../data.service";
IndicatorsCore(Highcharts);
IndicatorZigzag(Highcharts);

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  Highcharts: typeof Highcharts = Highcharts;
  chartData: Array<number[]> = [[1546441200000, 1045], [1546527600000, 1016]];
  constructor(private dataService: DataService) { }

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
}
