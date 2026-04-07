import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  constructor(private http: HttpClient) {}
  private readonly port = 'https://localhost:7290';

  OnPageReport(
    date_one: any,
    date_two: any,
    customerName: string
  ): Observable<any> {
    return this.http.get(
      `${this.port}/api/Customer/OnPageReport/${date_one}/${date_two}/${customerName}`
    );
  }
  CustomerAgingReport(): Observable<any> {
    return this.http.get(`${this.port}/api/Customer/CustomerAgingReport`);
  }
}
