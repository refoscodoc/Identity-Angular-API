import { Component, OnInit, Input} from "@angular/core";
import * as Highcharts from "highcharts/highstock";
import { Options } from "highcharts/highstock";

import IndicatorsCore from "highcharts/indicators/indicators";
import IndicatorZigzag from "highcharts/indicators/zigzag";

IndicatorsCore(Highcharts);
IndicatorZigzag(Highcharts);

@Component({
  selector: 'app-chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  Highcharts: typeof Highcharts = Highcharts;
  @Input() chartData: Array<number[]> = [[1546441200000, 1045], [1546527600000, 1016]];
  @Input() chartOptions: Options = {};

  ngOnInit() {
  }
}
