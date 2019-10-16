import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { User } from '../models/user';

const httpOptions = {
  headers: new HttpHeaders({
    Authorization: 'Bearer ' + JSON.parse(localStorage.getItem('tokens')).accessToken
  })
};

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
    return this.httpClient.get<User>(this.baseUrl + 'users', httpOptions);
  }

  getUser(id: string) {
    return this.httpClient.get<User>(`${this.baseUrl}users/${id}`, httpOptions);
  }
}
