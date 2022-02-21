import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'chart',
  templateUrl: './chart.component.html',
  styleUrls: ['./chart.component.css']
})
export class ChartComponent implements OnInit {

  constructor() { }

  @Input() tickerMockData: any;

  ngOnInit(): void {

  }

}
