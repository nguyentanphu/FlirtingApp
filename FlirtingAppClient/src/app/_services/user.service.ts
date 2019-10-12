import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = 'https://localhost:44367/api/users/';
  constructor(private httpClient: HttpClient) { }

  register(model) {
    return this.httpClient.post(this.baseUrl + 'create', model);
  }
}
