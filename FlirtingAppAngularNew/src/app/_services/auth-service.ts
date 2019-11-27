import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { JwtHelperService } from '@auth0/angular-jwt';
import { ILoginModel } from '../_models/users/login-model';

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

  login(model: ILoginModel) {
    return this.httpClient.post(this.baseUrl + 'login', model)
    .pipe(
      map((response: any) => {
        localStorage.setItem('tokens', JSON.stringify({
          accessToken: response.accessToken,
          refreshToken: response.refreshToken
        }));

        this.decodedAccessToken = this.jwtHelper.decodeToken(response.accessToken);
      })
    );
  }

  loggedIn() {
    const tokens = JSON.parse(localStorage.getItem('tokens'));
    if (!tokens) {
      return false;
    }
    return !this.jwtHelper.isTokenExpired(tokens.accessToken) && tokens.refreshToken;
  }
}

export function accessTokenGetter() {
  const tokens = JSON.parse(localStorage.getItem('tokens'));
  return tokens ? tokens.accessToken : null;
}
