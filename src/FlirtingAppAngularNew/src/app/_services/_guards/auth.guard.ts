import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { AuthService } from '../auth-service';
import { NotificationService } from '../notification-service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  constructor(
    private authService: AuthService, 
    private notiService: NotificationService,
    private router: Router) {}
  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (this.authService.loggedIn) {
      return true;
    }

    this.notiService.sendError('Please log in first!');
    this.router.navigate(['']);
    return false;
  }
}
