import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class authGuard implements CanActivate {
  
  constructor(private router: Router) {}

  canActivate() {
    const token = localStorage.getItem('token');

    if (token && token.trim() !== '') {
      return true;
    } else {
      this.router.navigateByUrl('/'); 
      return false;
    }
  }
}