import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { HighchartsChartModule } from "highcharts-angular";

import {
  FacebookLoginProvider,
  SocialLoginModule,
  GoogleLoginProvider,
  SocialAuthServiceConfig, SocialAuthService,
} from 'angularx-social-login';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './nav-menu/nav-menu.component';
import { HomeComponent } from './home/home.component';
import { CounterComponent } from './counter/counter.component';
import { FetchDataComponent } from './fetch-data/fetch-data.component';
import { TickerComponent } from './ticker/ticker.component';
import { ChartComponent } from './chart/chart.component';
import { MongoDbService } from "./mongoDbService/mongoDbService";
import { SelectManufacturerComponent } from './select-manufacturer/select-manufacturer.component';
import { LoginComponent } from './login/login.component';
import { LogoutComponent } from './logout/logout.component';
import { MfaComponent } from './mfa/mfa.component';
import { LoggedoutComponent } from './loggedout/loggedout.component';
import { NotFoundComponent } from './not-found/not-found.component';
import { AuthenticationPageComponent } from './authentication-page/authentication-page.component';

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
    LoginComponent,
    LogoutComponent,
    MfaComponent,
    LoggedoutComponent,
    NotFoundComponent,
    AuthenticationPageComponent,
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
      { path: "login", component: LoginComponent },
      { path: "logout", component: LogoutComponent },
      { path: "loggedout", component: LoggedoutComponent },
      { path: "mfa", component: MfaComponent },
      { path: "home", component: HomeComponent },
      { path: "authentication", component: AuthenticationPageComponent },
      { path: "**", component: NotFoundComponent },
    ])
  ],
  providers: [{
    provide: 'SocialAuthServiceConfig',
    useValue: {
      autoLogin: false,
      providers: [
        {
          id: GoogleLoginProvider.PROVIDER_ID,
          provider: new GoogleLoginProvider('318562243956-5n4e3nj5gqffhc1mpa4hsohpfmhjbjqe.apps.googleusercontent.com'),
        },
      ],
    } as SocialAuthServiceConfig,
  },
    SocialAuthService,
    MongoDbService],
  bootstrap: [AppComponent]
})
export class AppModule { }
