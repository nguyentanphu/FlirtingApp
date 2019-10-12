import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private baseUrl = 'https://localhost:44367/api/auth/';
  constructor(private httpClient: HttpClient) { }

  login(model) {
    return this.httpClient.post(this.baseUrl + 'login', model)
    .pipe(
      map((response: any) => {
        localStorage.setItem('tokens', JSON.stringify({
          accessToken: response.accessToken,
          refreshToken: response.refreshToken
        }));
      })
    );
  }

  

}

