import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class EnumServices {
  constructor(private http: HttpClient) {}
  private readonly port = 'https://localhost:7290';
  getPaymentStatus(): Observable<any> {
    return this.http.get<{ id: number; name: string }[]>(
      `${this.port}/api/Enum/GetPaymentStatus`
    );
  }

  getPaymentMethos(): Observable<any> {
    return this.http.get<{ id: number; name: string }[]>(
      `${this.port}/api/Enum/GetPaymentMethod`
    );
  }
}
