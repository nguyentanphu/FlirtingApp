import { Injectable } from '@angular/core';
import { ISignUpModel } from '../_models/users/sign-up-model';
import { HttpClient } from '@angular/common/http';
import { APIURL } from '../_models/api-url';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) { }
  createUser(signUpModel: ISignUpModel) {
    return this.http.post(APIURL.users.create, signUpModel);
  }
}
