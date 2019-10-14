import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../_services/user.service';
import { AlertifyService } from '../_services/alertify.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {
  @Output() registerDone = new EventEmitter();

  public model: any = {
    userName: '',
    password: ''
  };

  constructor(private userService: UserService, private alertify: AlertifyService) { }

  ngOnInit() {
  }

  register(registerForm: NgForm) {
    if (!registerForm.valid) {
      return;
    }
    this.userService.register(this.model).subscribe(result => {
      this.registerDone.emit();
      this.alertify.success('Registered successfully.');
    }, error => {
      this.alertify.error(error);
    });
  }

  cancelRegister() {
    this.registerDone.emit();
  }

}
