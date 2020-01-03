import { Injectable } from '@angular/core';
import { ISignUpModel } from '../_models/users/sign-up-model';
import { HttpClient } from '@angular/common/http';
import { APIURL } from '../_models/api-url';
import { take } from 'rxjs/operators';
import { IUserDetail } from '../_models/users/user-detail';
import { IUserAdditionalModel } from '../_models/users/user-additional-model';
import { IUserOverview } from '../_models/users/user-overview';
import { pipe } from 'rxjs';
import { IMemberListFilter } from '../_models/users/member-list-filter';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  constructor(private http: HttpClient) {}
  createUser(signUpModel: ISignUpModel) {
    return this.http.post(APIURL.users.create, signUpModel).pipe(take(1));
  }

  getUsersOverview(filter: IMemberListFilter) {
    return this.http.get<IUserOverview[]>
    (APIURL.users.get + `?distance=${filter.distance}&coordinates=${filter.coordinates[0]}&coordinates=${filter.coordinates[1]}`)
      .pipe(take(1));
  }

  getUserDetail(userId: string) {
    return this.http
      .get<IUserDetail>(APIURL.users.getUserDetail(userId))
      .pipe(take(1));
  }

  updateAdditionalDetail(additionalUserDetail: IUserAdditionalModel) {
    return this.http.put(APIURL.users.updateAdditionalDetail, additionalUserDetail)
      .pipe(take(1));
  }
}
