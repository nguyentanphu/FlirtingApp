import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Router } from '@angular/router';
import { isEmpty } from 'lodash';

import { NotificationService } from '../notification-service';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private router: Router, private notiService: NotificationService) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        switch (error.status) {

          case 401:
            this.router.navigate(['']);
            break;

          case 400:
            const errors = error.error.errors;
            if (isEmpty(errors)) {
              return this.sendMessageAndThrowError(error, 'Bad request! Validation error');
            }
            let message = '';
            // tslint:disable-next-line: forin
            for (const key in errors) {
              if (key === 'errorMessage') {
                message += errors[key];
              } else {
                message += `${key}: ${errors[key]}`;
              }
            }
            return this.sendMessageAndThrowError(error, message);

          default:
            return this.sendMessageAndThrowError(error, 'Unexpected error. Contact your admin!');
        }
      })
    );
  }

  sendMessageAndThrowError(error, message) {
    this.notiService.sendError(message);
    return throwError(error);
  }
}

