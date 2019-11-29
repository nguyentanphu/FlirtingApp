import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Gender, ISignUpModel } from 'src/app/_models/users/sign-up-model';
import { enumToKeyValueArray } from 'src/app/_services/helpers/enum-helpers';
import { UserService } from 'src/app/_services/user-service';
import { AuthService } from 'src/app/_services/auth-service';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/_services/notification-service';
import { maxDate } from 'src/app/_services/validators/birth-day.validator';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  signUpFormGroup: FormGroup;
  constructor(
    private formBuilder: FormBuilder, 
    private userService: UserService,
    private authService: AuthService,
    private notiService: NotificationService,
    private router: Router
  ) {
    this.signUpFormGroup = this.formBuilder.group({
      firstName: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      lastName: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      userName: ['', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(20)])],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(6)])],
      dateOfBirth: [null, Validators.compose([Validators.required, maxDate(new Date(2000, 1, 1))])],
      gender: [Gender.Unknown, Validators.required]
    });
  }
  genderOptions = enumToKeyValueArray<number>(Gender);


  ngOnInit() {}

  signUpSubmit() {
    if (!this.signUpFormGroup.valid) {
      return;
    }
    const signUpModel = this.signUpFormGroup.value as ISignUpModel;
    this.userService.createUser(signUpModel).subscribe(() => {
      this.notiService.sendSuccess('Sign up succeeded');
      this.authService.login({userName: signUpModel.userName, password: signUpModel.password}).subscribe(() => {
        this.router.navigate(['/members']);
      });
    });
  }
}
