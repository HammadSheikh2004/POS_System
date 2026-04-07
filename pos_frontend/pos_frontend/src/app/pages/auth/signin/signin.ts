import { Component } from '@angular/core';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { InputElements } from '../../../components/input-elements/input-elements';
import { ButtonElement } from '../../../components/button-element/button-element';
import { AuthService } from '../../../services/auth-service';
import { CookieService } from 'ngx-cookie-service';
import { Router } from '@angular/router';
@Component({
  selector: 'app-signin',
  imports: [ReactiveFormsModule, CommonModule, InputElements, ButtonElement],
  templateUrl: './signin.html',
  styleUrl: './signin.css',
})
export class Signin {
  storeImage = '../../../../assets/images/store.png';
  signinForm: FormGroup;
  loading:boolean = false;
  constructor(
    private authService: AuthService,
    private cookieService: CookieService,
    private router: Router,
  ) {
    this.signinForm = new FormGroup({
      userName: new FormControl('', [Validators.required]),
      password: new FormControl('', [Validators.required]),
    });
  }

  showPassword = false;

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  get userNameControl(): FormControl {
    return this.signinForm.get('userName') as FormControl;
  }

  get passwordControl(): FormControl {
    return this.signinForm.get('password') as FormControl;
  }

  onSubmit() {
    if (this.signinForm.valid) {
      this.loading = true;
      const { userName, password } = this.signinForm.value;
      this.authService.login(userName, password).subscribe({
        next: (response) => {

          if (response?.token) {
            localStorage.setItem('token', response.token);

            this.router.navigate(['/admin']);
          } else {
            const token = localStorage.getItem('token');
            if (token) {
              const payload = JSON.parse(atob(token.split('.')[1]));
              if (payload.role === 'Admin') {
                this.router.navigate(['/admin/dashboard']);
              }
            }
          }
          this.loading = false;
        },
        error: (err) => {
          this.loading = false;
          console.error('Login failed:', err);
        },
      });
    } else {
      this.loading = false;
      console.error('Form is invalid');
    }
  }
}
