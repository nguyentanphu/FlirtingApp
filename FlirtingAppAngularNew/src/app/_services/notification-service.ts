import { MatSnackBar } from '@angular/material';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  constructor(private snackBar: MatSnackBar) {

  }
  sendSuccess(message: string, duration = 2000) {
    this.snackBar.open(message, null, { duration, panelClass: 'success-snackbar', horizontalPosition: 'right' });
  }

  sendError(message: string, duration = 2000) {
    this.snackBar.open(message, null, { duration, panelClass: 'error-snackbar', horizontalPosition: 'right' });
  }

  sendWarning(message: string, duration = 2000) {
    this.snackBar.open(message, null, { duration, panelClass: 'warning-snackbar', horizontalPosition: 'right' });
  }
}
