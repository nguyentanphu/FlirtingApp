import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth-service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  constructor(private authService: AuthService) { }
  isRegistering = false;

  public get loggedIn() {
    return this.authService.loggedIn;
  }

  ngOnInit() {
  }

}
