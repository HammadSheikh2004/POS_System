import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environment/environment.production';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private http: HttpClient) {}
  private readonly port = 'https://localhost:7290';
  private readonly apiUrl = environment.apiUrl;

  OnPageReport(
    date_one: any,
    date_two: any,
    customerName: string
  ): Observable<any> {
    return this.http.get(
      `${this.apiUrl}/Customer/OnPageReport/${date_one}/${date_two}/${customerName}`
    );
  }
  CustomerAgingReport(): Observable<any> {
    return this.http.get(`${this.apiUrl}/Customer/CustomerAgingReport`);
  }
}
