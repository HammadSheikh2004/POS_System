import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  constructor(private http: HttpClient, private cookieService:CookieService) {}

  private readonly port = 'https://localhost:7290';

  login(userName: string, password: string): Observable<any> {
    const body = { userName, password };

    return this.http.post(`${this.port}/api/Auth/Signin`, body, {
      withCredentials: true, 
    });
  }
}
