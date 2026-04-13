import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environment/environment.production';

@Injectable({
  providedIn: 'root',
})
export class EnumServices {
  constructor(private http: HttpClient) {}
  private readonly port = 'https://localhost:7290';
  private readonly apiUrl = environment.apiUrl;
  getPaymentStatus(): Observable<any> {
    return this.http.get<{ id: number; name: string }[]>(
      `${this.apiUrl}/Enum/GetPaymentStatus`
    );
  }

  getPaymentMethos(): Observable<any> {
    return this.http.get<{ id: number; name: string }[]>(
      `${this.apiUrl}/Enum/GetPaymentMethod`
    );
  }
}
