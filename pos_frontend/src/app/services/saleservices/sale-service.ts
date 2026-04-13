import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environment/environment.production';
@Injectable({
  providedIn: 'root',
})
export class SaleService {
  constructor(private http: HttpClient) {}
  private readonly port = 'https://localhost:7290';
  private readonly apiUrl = environment.apiUrl;
  sale = signal<any[]>([]);

  AddSales(formData: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Sale/AddSale`, formData);
  }

  DisplayService(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Sale/GetAllSale`);
  }

  DailySaleReport(): Observable<any> {
    return this.http.get<any[]>(`${this.apiUrl}/Sale/DailySaleReport`);
  }

  CustomerSaleReport(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.apiUrl}/Sale/CustomerSaleHistoryReport`
    );
  }

  ProductWiseSaleReport(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Sale/ProductWiseSaleReport`);
  }

  TodaySaleReport(today_date: string): Observable<any> {
    return this.http.get<any>(
      `${this.apiUrl}/Sale/TodaySaleReport/${today_date}`
    );
  }

  FetchALLSale() {
    this.DisplayService().subscribe({
      next: (res: any) => {
        var data = Array.isArray(res) ? res : res.data;
        this.sale.set(data);
      },
      error(err) {
        console.log(err);
      },
    });
  }
}
