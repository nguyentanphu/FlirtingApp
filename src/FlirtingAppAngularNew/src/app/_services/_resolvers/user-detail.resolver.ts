import { Resolve, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';
import { IUserDetail } from 'src/app/_models/users/user-detail';
import { UserService } from '../user-service';
import { Observable } from 'rxjs';
import { AuthService } from '../auth-service';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UserDetailResolver implements Resolve<IUserDetail> {
  constructor(private authService: AuthService, private userService: UserService) {}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IUserDetail> {
    const userId = this.authService.decodedAccessToken.user_id;
    return this.userService.getUserDetail(userId);
  }
}
