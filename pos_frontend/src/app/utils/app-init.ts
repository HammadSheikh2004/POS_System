import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';

export function appInitializer(cookie: CookieService, router: Router) {
  return () => {
    const token = cookie.get('token');

    if (!token) {
      router.navigateByUrl('/');
      return;
    }

    try {
      const payload = JSON.parse(atob(token.split('.')[1]));
      if (payload?.role === 'Admin') {
        router.navigateByUrl('/admin');
      } else {
        cookie.delete('token', '/');
        router.navigateByUrl('/');
      }
    } catch {
      cookie.delete('token', '/');
      router.navigateByUrl('/');
    }
  };
}
