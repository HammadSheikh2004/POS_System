import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environment/environment.production';
@Injectable({
  providedIn: 'root',
})
export class Inventoryservice {
  constructor(private http: HttpClient) {}
  private readonly port = 'https://localhost:7290';
  private readonly apiUrl = environment.apiUrl;
  pro = signal<any[]>([]);

  addProduct(formData: any[]): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post(`${this.apiUrl}/Inventory/AddInventory`, formData, {
      headers,
    });
  }

  displayProducts(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Inventory/FetchAllProducts`);
  }

  fetchProductById(id: string): Observable<any> {
    return this.http.get<any>(
      `${this.apiUrl}/Inventory/FetchProductsById/${id}`
    );
  }

  updateProduct(formData: any, id: string): Observable<any> {
    return this.http.put<any>(
      `${this.apiUrl}/Inventory/UpdateProduct/${id}`,
      formData
    );
  }

  deleteProduct(id: number): Observable<any> {
    return this.http.delete<any>(
      `${this.apiUrl}/Inventory/DeleteProduct/${id}`
    );
  }

  AddPurchaseOrder(formData: any): Observable<any> {
    return this.http.post<any>(
      `${this.apiUrl}/Inventory/AddPurchaseOrder`,
      formData
    );
  }

  displayPurchaseOrders(): Observable<any[]> {
    return this.http.get<any[]>(
      `${this.apiUrl}/Inventory/FetchAllPurchaseOrders`
    );
  }

  displayProductName(): Observable<any[]> {   
    return this.http.get<any[]>(`${this.apiUrl}/Inventory/FetchProductsName`)
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
