import { Component, OnInit, Output, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Component({
  selector: 'app-ticker',
  templateUrl: './ticker.component.html',
  styleUrls: ['./ticker.component.css']
})

@Injectable()
export class MongoDbService {

  baseUrl: string = "http://localhost:5001";

  @Output()
  tickerMockData = [
    { name: "Mobiles", value: 10.5 },
    { name: "Laptop", value: 5.5 },
    { name: "AC", value: 15 },
    { name: "Headset", value: 1.5 },
    { name: "Fridge", value: 17.0 }
  ];

  constructor(private http: HttpClient) { }

  ngOnInit(): void {
  }

  callMongoApi(manufacturer: string): Observable<any> {
    return this.http.get('http://localhost:5001' + 'MongoDb' + manufacturer)
  }

}
