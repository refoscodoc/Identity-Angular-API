import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TickerModel } from "./mongoDbService/tickerModel";
import {Observable} from "rxjs";
import {environment} from "../environments/environment";

@Injectable({
  providedIn: 'root'
})
export class DataService {

  // private serverUrl = `${environment.API_URL}/MongoDb`;

  private serverUrl = "https://localhost:5002/MongoDb";

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  };
  constructor(private http: HttpClient) {

  }

  getData(): Observable<TickerModel[]> {
    return this.http.get<TickerModel[]>(this.serverUrl, this.httpOptions)
  }
}
