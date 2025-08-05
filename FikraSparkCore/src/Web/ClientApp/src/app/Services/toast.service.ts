import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';


export interface Toast {
  message: string;
  type?: 'success' | 'error' | 'info' | 'warning';
  duration?: number;
}

@Injectable({
  providedIn: 'root',
})
export class ToastService {
  private toastSubject = new Subject<Toast | null>();
  toast$ = this.toastSubject.asObservable();

  show(message: string, type: Toast['type'] = 'info', duration = 3000) {
    this.toastSubject.next({ message, type, duration });

    setTimeout(() => {
      this.toastSubject.next(null);
    }, duration);
  }
}
