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
    const accessToken = JSON.parse(localStorage.getItem('accessToken'));
    const refreshToken = JSON.parse(localStorage.getItem('refreshToken'));
    return !!accessToken && !!refreshToken;
  }

  logout() {
    localStorage.removeItem('accessToken');
    localStorage.removeItem('refreshToken');
  }

}
