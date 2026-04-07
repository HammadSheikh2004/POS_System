import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class Inventoryservice {
  constructor(private http: HttpClient) {}
  private readonly port = 'https://localhost:7290';
  pro = signal<any[]>([]);

  addProduct(formData: any[]): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post(`${this.port}/api/Inventory/AddInventory`, formData, {
      headers,
    });
  }

  displayProducts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.port}/api/Inventory/FetchAllProducts`);
  }

  fetchProductById(id: string): Observable<any> {
    return this.http.get<any>(
      `${this.port}/api/Inventory/FetchProductsById/${id}`
    );
  }

  updateProduct(formData: any, id: string): Observable<any> {
    return this.http.put<any>(
      `${this.port}/api/Inventory/UpdateProduct/${id}`,
      formData
    );
  }

  deleteProduct(id: number): Observable<any> {
    return this.http.delete<any>(
      `${this.port}/api/Inventory/DeleteProduct/${id}`
    );
  }

  AddPurchaseOrder(formData: any): Observable<any> {
    return this.http.post<any>(
      `${this.port}/api/Inventory/AddPurchaseOrder`,
      formData
    );
  }

  displayPurchaseOrders(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.port}/api/Inventory/FetchAllPurchaseOrders`
    );
  }

  displayProductName(): Observable<any[]> {   
    return this.http.get<any[]>(`${this.port}/api/Inventory/FetchProductsName`)
  }

  fetchProducts() {
    this.displayProducts().subscribe({
      next: (res) => {
        this.pro.set(res);
      },
      error: (err) => {
        console.log(err);
      },
    });
  }

}
