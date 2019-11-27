import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { NotificationService } from '../notification-service';
import { Router } from '@angular/router';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private notiService: NotificationService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {

        if (error.status === 401) {
          this.router.navigate(['']);
        } else if (error.status === 400) {
          this.notiService.sendError('Bad request! Validation error');
        } else {
          this.notiService.sendError('Unexpected error. Contact your admin!');
        }

        return throwError(error);
      })
    );
  }
}
