import { Component, OnInit } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { Router, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.html',
  imports:[RouterOutlet],
  styleUrls: ['./app.css'],
})
export class App implements OnInit {
  constructor(
    private cookieService: CookieService,
    private router: Router
  ) {}

   ngOnInit() {
    const token = this.cookieService.get('token');
    if (token) {
      const payload = JSON.parse(atob(token.split('.')[1]));
      if (payload.role === 'Admin') {
        this.router.navigate(['/admin']);
      }
    }
  }
}
