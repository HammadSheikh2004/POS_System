import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';
import {environment} from '../environment/environment.production';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private cookieService:CookieService) {}

  private readonly port = 'https://localhost:7290';
  private apiUrl = environment.apiUrl;
  login(userName: string, password: string): Observable<any> {
    const body = { userName, password };

    return this.http.post(`${this.apiUrl}/Auth/Signin`, body, {
      withCredentials: true,
    });
  }
}
