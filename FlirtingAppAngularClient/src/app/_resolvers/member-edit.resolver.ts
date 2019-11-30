import { catchError } from 'rxjs/operators';
import { AlertifyService } from '../_services/alertify.service';
import { UserService } from '../_services/user.service';
import { Injectable } from '@angular/core';
import { Resolve, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { User } from '../models/user';
import { Observable, of } from 'rxjs';
import { AuthService } from '../_services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class MemberEditResolver implements Resolve<User> {
    constructor(
        private router: Router,
        private userService: UserService,
        private authService: AuthService,
        private alertify: AlertifyService
    ) {
    }
    resolve(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): User | Observable<User> | Promise<User> {
        return this.userService.getUser(this.authService.decodedAccessToken.user_id).pipe(
            catchError(error => {
                this.alertify.error(error);
                this.router.navigate(['/members']);
                return of(null);
            })
        );
    }
}
