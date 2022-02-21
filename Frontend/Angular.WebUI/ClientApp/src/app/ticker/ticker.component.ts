import { Component, Output } from '@angular/core';
import { MongoDbService } from "../mongoDbService/MongoDbService";

import { TickerModel } from "../mongoDbService/tickerModel";

@Component({
  selector: 'app-ticker',
  templateUrl: './ticker.component.html',
  styleUrls: ['./ticker.component.css']
})

export class TickerComponent {

  manufacturer: string = "sony";

  @Output()
  tickers: TickerModel[] = [];

  loading: boolean = false;
  errorMessage: string = "";

  constructor(private mongoService: MongoDbService) {
  }

  public getTickers() {
    this.loading = true;
    this.errorMessage = "";
    this.mongoService.callMongoApi(this.manufacturer)
      .subscribe(
        (response) => {                           //next() callback
          console.log('response received')
          this.tickers = response;
        },
        (error) => {                              //error() callback
          console.error('Request failed with error')
          this.errorMessage = error;
          this.loading = false;
        },
        () => {                                   //complete() callback
          console.error('Request completed')      //This is actually not needed
          this.loading = false;
        })
  }

}
