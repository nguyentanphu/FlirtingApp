import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NotificationService } from 'src/app/_services/notification-service';
import { AuthService } from 'src/app/_services/auth-service';

@Component({
  selector: 'app-nav-login',
  templateUrl: './nav-login.component.html',
  styleUrls: ['./nav-login.component.scss']
})
export class NavLoginComponent implements OnInit {
  loginFormGroup = this.formBuilder.group({
    userName: ['', Validators.compose([Validators.required, Validators.minLength(4), Validators.maxLength(20)])],
    password: ['', Validators.compose([Validators.required, Validators.minLength(6), Validators.pattern(new RegExp('^\\S*$'))])]
  });

  constructor(
    private formBuilder: FormBuilder,
    private notiService: NotificationService,
    private authService: AuthService
  ) { }

  ngOnInit() {
  }

  loginSubmit() {
    if (!this.loginFormGroup.valid) {
      return this.notiService.sendError(
        `Username must be from 4 to 20 characters.\nPassword must be least 6 characters and do not contain spaces`);
    }

    this.authService.login(this.loginFormGroup.value).subscribe(tokens => {
      this.notiService.sendSuccess('Login succeeded');
    });
  }
}

