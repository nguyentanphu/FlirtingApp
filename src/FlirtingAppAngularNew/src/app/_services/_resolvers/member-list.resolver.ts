import { Resolve, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { IUserOverview } from 'src/app/_models/users/user-overview';
import { Observable, of } from 'rxjs';
import { UserService } from '../user-service';
import { Injectable } from '@angular/core';
import { IMemberListFilter } from 'src/app/_models/users/member-list-filter';

@Injectable({
  providedIn: 'root'
})
export class MemberListResolver implements Resolve<IUserOverview[]> {
  constructor(private userService: UserService) {}
  resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<IUserOverview[]> {
    
    return of(null);
  }
}
