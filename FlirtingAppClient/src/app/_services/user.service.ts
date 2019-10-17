import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl = environment.apiUrl;
  constructor(private httpClient: HttpClient) { }

  register(model) {
    return this.httpClient.post(this.baseUrl + 'create', model);
  }

  getUsers() {
    return this.httpClient.get<User[]>(this.baseUrl + 'users');
  }

  getUser(id: string) {
    return this.httpClient.get<User>(`${this.baseUrl}users/${id}`);
  }
}
