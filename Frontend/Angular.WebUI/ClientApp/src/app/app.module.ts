import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HighchartsChartModule } from "highcharts-angular";

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TickerComponent } from './ticker/ticker.component';
import { ChartComponent } from './chart/chart.component';
import { MongoDbService } from "./mongoDbService/mongoDbService";
import { SelectManufacturerComponent } from './select-manufacturer/select-manufacturer.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomeComponent,
    CounterComponent,
    FetchDataComponent,
    TickerComponent,
    ChartComponent,
    SelectManufacturerComponent,
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    HighchartsChartModule,
    RouterModule.forRoot([
      { path: '', component: HomeComponent, pathMatch: 'full' },
      { path: 'counter', component: CounterComponent },
      { path: 'fetch-data', component: FetchDataComponent },
      { path: 'nav-menu', component: NavMenuComponent },
      { path: 'ticker', component: TickerComponent },
      { path: 'chart', component: ChartComponent },
      { path: 'app-select-manufacturer', component: SelectManufacturerComponent },
    ])
  ],
  providers: [MongoDbService],
  bootstrap: [AppComponent]
})
export class AppModule { }
