import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthClient, RegisterRequest } from '../../web-api-client';
import { Router } from '@angular/router';
import { ToastService } from 'src/app/Services/toast.service';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;
  loading = false;
  error: string | null = null;
  showPassword = false;
  showConfirmPassword = false;

  constructor(
    private fb: FormBuilder,
    private authClient: AuthClient,
    private router: Router,
    private toastService: ToastService
  ) {
    this.registerForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }

  register() {
    if (this.registerForm.invalid) return;

    const { email, password, confirmPassword } = this.registerForm.value;

    if (password !== confirmPassword) {
      this.error = "Passwords do not match.";
      this.toastService.show('Passwords do not match.', 'error');
      return;
    }

    this.loading = true;
    this.error = null;

    const request = new RegisterRequest({ email, password });

    this.authClient.postApiAuthApiAccountRegister(request).subscribe({
      next: () => {
        this.loading = false;
        this.toastService.show('Registration successful! You can now log in.', 'success');
        this.router.navigate(['/login']);

      },
      error: (err) => {
        this.loading = false;
        this.error = err?.error?.message || 'Registration failed. Please try again.';
        this.toastService.show('Registration failed: ' + err.message, 'error');
      }
    });
  }
}