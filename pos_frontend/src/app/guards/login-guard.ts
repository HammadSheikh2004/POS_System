// loginGuard.ts
import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';

@Injectable({
  providedIn: 'root',
})
export class loginGuard implements CanActivate {
  constructor(private router: Router) {}

  canActivate() {
    const token = localStorage.getItem('token');;
    if (token && token.trim() !== '') {
      this.router.navigate(['/admin/dashboard']);
      return false;
    }
    return true;
  }
}
