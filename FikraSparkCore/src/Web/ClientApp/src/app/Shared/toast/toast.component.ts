import { Component, OnDestroy, OnInit } from '@angular/core';
import { ToastService, Toast } from '../../Services/toast.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrl: './toast.component.css'
})
export class ToastComponent implements OnInit, OnDestroy {
 toast: Toast | null = null;
 private subscription: Subscription | undefined;


  constructor(private toastService: ToastService) {}

  ngOnInit() {
    this.toastService.toast$.subscribe((toast) => {
      this.toast = toast;
    });
  }
  ngOnDestroy() {
    this.subscription?.unsubscribe();
  }

  close() {
    this.toast = null;
  }

  getIconClass(type: string | undefined): string {
   switch(type) {
    case 'success': return 'ph ph-check-circle-fill';
    case 'error': return 'ph ph-x-circle-fill';
    case 'warning': return 'ph ph-warning-fill';
    case 'info': 
    default:
      return 'ph ph-info-fill';
  }
}
}
