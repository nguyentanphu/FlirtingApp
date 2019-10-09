import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseUrl = 'https://localhost:44367/api/auth/login';
  constructor(private httpClient: HttpClient) { }

  login(model) {
    return this.httpClient.post(this.baseUrl, model)
    .pipe(
      map((response: any) => {
        localStorage.setItem('accessToken', response.accessToken);
        localStorage.setItem('refreshToken', response.refreshToken);
      })
    );
  }

}

