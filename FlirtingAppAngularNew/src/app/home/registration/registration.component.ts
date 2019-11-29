import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Gender } from 'src/app/_models/users/sign-up-model';
import { enumToKeyValueArray } from 'src/app/_services/helpers/enum-helpers';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.scss']
})
export class RegistrationComponent implements OnInit {
  signUpFormGroup: FormGroup;
  constructor(private formBuilder: FormBuilder) {
    this.signUpFormGroup = this.formBuilder.group({
      firstName: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      lastName: ['', Validators.compose([Validators.required, Validators.minLength(3)])],
      userName: ['', Validators.compose([Validators.required, Validators.minLength(6), Validators.maxLength(20)])],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(6)])],
      dateOfBirth: [null, Validators.required],
      gender: [Gender.Unknown, Validators.required]
    });
  }
  genderOptions = enumToKeyValueArray<number>(Gender);


  ngOnInit() {}
}
