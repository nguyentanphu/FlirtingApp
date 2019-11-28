import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject } from 'rxjs';
import { catchError, switchMap, filter, take } from 'rxjs/operators';
import { accessTokenGetter, refreshTokenGetter } from '../helpers/token-helpers';
import { AuthService } from '../auth-service';
import { ITokenModel } from 'src/app/_models/users/token-model';
import { Injectable } from '@angular/core';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
  constructor(private authService: AuthService) { }

  private isRefreshing = false;
  private refreshTokenSubject: BehaviorSubject<string | null> = new BehaviorSubject(null);

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    const accessToken = accessTokenGetter();
    const refreshToken = refreshTokenGetter();
    if (accessToken) {
      this.addAccessToken(req, accessToken);
    }

    return next.handle(req).pipe(
      catchError(error => {
        if (error.status === 401 && refreshToken) {
          return this.handleExchangeTokens(req, next, { accessToken, refreshToken });
        }
        return throwError(error);
      })
    );
  }

  private handleExchangeTokens(req: HttpRequest<any>, next: HttpHandler, tokens: ITokenModel) {
    if (!this.isRefreshing) {
      this.isRefreshing = true;
      return this.authService.exchangeTokens(tokens).pipe(
        switchMap(newTokens => {
          this.isRefreshing = false;
          this.refreshTokenSubject.next(newTokens.accessToken);
          return next.handle(this.addAccessToken(req, newTokens.accessToken));
        })
      );
    } else {
      return this.refreshTokenSubject.pipe(
        filter(accessToken => accessToken !== null),
        take(1),
        switchMap(accessToken => {
          return next.handle(this.addAccessToken(req, accessToken));
        })
      );
    }
  }


  private addAccessToken(req: HttpRequest<any>, token: string) {
    return req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`
      }
    });
  }
}
