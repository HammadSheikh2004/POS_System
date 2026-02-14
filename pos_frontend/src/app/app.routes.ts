import { Routes } from '@angular/router';
import { Signin } from './pages/auth/signin/signin';
import { loginGuard } from './guards/login-guard';
import { authGuard } from './guards/auth-guard';

export const routes: Routes = [
  {
    path: '',
    component: Signin,
    canActivate:[loginGuard]
  },
  {
    path: 'admin',
    canActivate:[authGuard],
    loadChildren: () =>
      import('./dashboard/module/admin/admin.module').then(
        (m) => m.AdminModule
      ),
  },
  
{ path: '**', redirectTo: '' },
];

