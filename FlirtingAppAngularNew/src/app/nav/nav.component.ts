import { Component, OnInit } from '@angular/core';
import { AuthService } from '../_services/auth-service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  constructor(private authService: AuthService) { }

  ngOnInit() {
  }

  public get loggedIn() {
    return this.authService.loggedIn();
  }
}
