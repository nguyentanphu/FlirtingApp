import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';
import { AlertifyService } from '../_services/alertify.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(
    private authService: AuthService,
    private alertify: AlertifyService,
    private router: Router) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(value => {
      this.alertify.success('Login successfully.');
    }, errorMessage => {
      this.alertify.error(errorMessage);
    }, () => {
      this.router.navigate(['/members']);
    });
  }

  loggedIn() {
    return this.authService.loggedIn();
  }

  public get loggedInUserName() {
    if (this.authService.decodedAccessToken) {
      return this.authService.decodedAccessToken.sub;
    }
    return '';
  }

  logout() {
    localStorage.removeItem('tokens');
    this.alertify.message('Logged out.');
    this.router.navigate(['/home']);
  }

}
