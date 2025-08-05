import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthClient, LoginRequest } from '../../web-api-client';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/Services/toast.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent{
  loginForm: FormGroup;
  loading = false;
  error = '';
  showPassword: boolean = false;

  constructor(private fb: FormBuilder, private authClient: AuthClient, private router: Router, private toastService: ToastService) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
    });
  }

  login() {
    if (this.loginForm.invalid) return;

    this.loading = true;
    this.error = '';

    const request = new LoginRequest(this.loginForm.value);
    this.authClient.postApiAuthApiAccountLogin(request).subscribe({
      next: (resp) => {
        this.setToken(resp);
        this.toastService.show('Login successful!', 'success'),
        this.router.navigate(['/idea']);
      },
      error: (err) => {
        this.error = 'Invalid email or password.';
        this.toastService.show('Login failed: ' + err.message, 'error')
        this.loading = false;
      },
    });
  }

  setToken(resp: any): void {
  const token = resp?.token;
  if (token) {
    localStorage.setItem('auth_token', token);
  } else {
    console.warn('No token found in response:', resp);
  }
}

}