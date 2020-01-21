import { Component } from '@angular/core';
import { AuthService } from 'src/app/_services/auth-service';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/_services/notification-service';

@Component({
  selector: 'app-nav-logout',
  templateUrl: './nav-logout.component.html',
  styleUrls: ['./nav-logout.component.scss']
})
export class NavLogoutComponent {

  constructor(
    private authService: AuthService,
    private router: Router,
    private notiService: NotificationService
  ) { }

  public get loggedInUserName() {
    if (this.authService.decodedAccessToken) {
      return this.authService.decodedAccessToken.sub;
    }
    return '';
  }

  public logout() {
    this.authService.logout().subscribe(() => {
      this.notiService.sendSuccess('Logout succeeded.');
      this.router.navigate(['']);
    });

  }
}
