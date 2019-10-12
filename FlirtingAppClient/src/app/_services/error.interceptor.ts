import { Injectable } from '@angular/core';
import { HttpInterceptor, HTTP_INTERCEPTORS } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  intercept(
    req: import('@angular/common/http').HttpRequest<any>,
    next: import('@angular/common/http').HttpHandler
  ): import('rxjs').Observable<import('@angular/common/http').HttpEvent<any>> {
    return next.handle(req).pipe(
      catchError(error => {
        const defaultErrorMessage = 'Unexpected error. Contact your admin!';
        if (error.status === 401) {
          return throwError('Unauthorized request');
        }
        if (error.status === 400) {
          const serverError = error.error;
          let validationMessage = '';
          if (serverError && typeof serverError === 'object') {
            // Check if there're any model state validation errors
            const modelInvalidStates = serverError.errors;
            if (modelInvalidStates && typeof modelInvalidStates === 'object') {
              for (const key of Object.keys(modelInvalidStates)) {
                validationMessage += modelInvalidStates[key] + '\n';
              }
            }
          }
          return throwError(
            validationMessage || serverError || defaultErrorMessage
          );
        }
        return throwError(defaultErrorMessage);
      })
    );
  }
}

export const ErrorInterceptorProvider = {
  provide: HTTP_INTERCEPTORS,
  useClass: ErrorInterceptor,
  multi: true
};
