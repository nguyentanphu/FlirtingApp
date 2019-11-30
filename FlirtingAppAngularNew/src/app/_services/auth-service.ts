import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map, tap, take } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ILoginModel } from '../_models/users/login-model';
import { APIURL } from '../_models/api-url';
import { Observable } from 'rxjs';
import { TokensModel } from '../_models/auth/tokens-model';
import { ITokenModel } from '../_models/users/token-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public decodedAccessToken: AccessTokenClaims;
  private jwtHelper = new JwtHelperService();
  constructor(private httpClient: HttpClient) {
    const accessToken = this.accessTokenGetter();
    const refreshToken = this.refreshTokenGetter();
    if (accessToken && refreshToken) {
      this.decodedAccessToken = this.jwtHelper.decodeToken(accessToken);
    }
  }
  accessTokenGetter() {
    return localStorage.getItem('accessToken');
  }
  refreshTokenGetter() {
    return localStorage.getItem('refreshToken');
  }

  login(model: ILoginModel): Observable<TokensModel> {
    return this.httpClient.post<TokensModel>(APIURL.auth.login, model)
      .pipe(
        take(1),
        tap(tokens => this.storeTokens(tokens))
      );
  }

  exchangeTokens(oldTokens: ITokenModel) {
    return this.httpClient.post<ITokenModel>(APIURL.auth.exchangeTokens, oldTokens)
      .pipe(
        take(1),
        tap(tokens => this.storeTokens(tokens))
      );
  }

  get loggedIn() {
    const token = localStorage.getItem('accessToken');
    if (!token) {
      return false;
    }
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout() {
    return this.httpClient.post(APIURL.auth.logout, {})
      .pipe(
        take(1),
        tap(() => {
          localStorage.removeItem('accessToken');
          localStorage.removeItem('refreshToken');
        })
      );
  }

  private storeTokens(tokens) {
    localStorage.setItem('accessToken', tokens.accessToken);
    localStorage.setItem('refreshToken', tokens.refreshToken);
    this.decodedAccessToken = this.jwtHelper.decodeToken(tokens.accessToken);
  }
}


interface AccessTokenClaims {
  app_user_id: string;
  user_id: string;
  aud: string;
  exp: number;
  iat: number;
  iss: string;
  nbf: number;
  jti: string;
  rol: string;
  sub: string;
}

