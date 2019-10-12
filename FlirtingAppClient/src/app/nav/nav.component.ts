import { AuthService } from './../_services/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {
  model: any = {};
  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  login() {
    this.authService.login(this.model).subscribe(value => {
      console.log('login successfully!', value);
    }, error => {
      console.log(error);
    });
  }

  loggedIn() {
    const tokens = JSON.parse(localStorage.getItem('tokens'));
    return tokens && !!tokens.accessToken && !!tokens.refreshTokens;
  }

  logout() {
    localStorage.removeItem('tokens');
  }

}
