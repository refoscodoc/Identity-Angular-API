import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable()
export class MongoDbService {

  baseUrl: string = "http://localhost:5002/MongoDb/";

  constructor(private http: HttpClient) { }

  callMongoApi(manufacturer: string): Observable<any> {
    return this.http.get(this.baseUrl + manufacturer)
  }

}
