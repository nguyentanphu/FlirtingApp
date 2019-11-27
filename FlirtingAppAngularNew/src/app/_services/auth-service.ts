import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ILoginModel } from '../_models/users/login-model';
import { APIURL } from '../_models/api-url';
import { Observable } from 'rxjs';
import { TokensModel } from '../_models/auth/tokens-model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public decodedAccessToken = null;
  private baseUrl = 'https://localhost:44367/api/auth/';
  private jwtHelper = new JwtHelperService();
  constructor(private httpClient: HttpClient) {
    const tokens = JSON.parse(localStorage.getItem('tokens'));
    if (tokens && tokens.accessToken) {
      this.decodedAccessToken = this.jwtHelper.decodeToken(tokens.accessToken);
    }
  }

  login(model: ILoginModel): Observable<TokensModel> {
    return this.httpClient.post<TokensModel>(APIURL.auth.login, model)
    .pipe(
      map((response) => {
        localStorage.setItem('accessToken', response.accessToken);
        localStorage.setItem('refreshToken', response.refreshToken);
        this.decodedAccessToken = this.jwtHelper.decodeToken(response.accessToken);
        return response;
      })
    );
  }

  loggedIn() {
    const token = localStorage.getItem('accessToken');
    if (!token) {
      return false;
    }
    return !this.jwtHelper.isTokenExpired(token);
  }
}

export function accessTokenGetter() {
  return localStorage.getItem('accessToken');
}
