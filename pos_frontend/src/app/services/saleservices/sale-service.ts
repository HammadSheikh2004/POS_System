import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SaleService {
  constructor(private http: HttpClient) {}
  private readonly port = 'https://localhost:7290';
  sale = signal<any[]>([]);

  AddSales(formData: any): Observable<any> {
    return this.http.post(`${this.port}/api/Sale/AddSale`, formData);
  }

  DisplayService(): Observable<any[]> {
    return this.http.get<any[]>(`${this.port}/api/Sale/GetAllSale`);
  }

  DailySaleReport(): Observable<any> {
    return this.http.get<any[]>(`${this.port}/api/Sale/DailySaleReport`);
  }

  CustomerSaleReport(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.port}/api/Sale/CustomerSaleHistoryReport`
    );
  }

  ProductWiseSaleReport(): Observable<any[]> {
    return this.http.get<any[]>(`${this.port}/api/Sale/ProductWiseSaleReport`);
  }

  TodaySaleReport(today_date: string): Observable<any> {
    return this.http.get<any>(
      `${this.port}/api/Sale/TodaySaleReport/${today_date}`
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
