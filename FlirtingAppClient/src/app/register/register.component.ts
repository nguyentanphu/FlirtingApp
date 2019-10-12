import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { NgForm } from '@angular/forms';
import { UserService } from '../_services/user.service';

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

  constructor(private userService: UserService) { }

  ngOnInit() {
  }

  register(registerForm: NgForm) {
    if (!registerForm.valid) {
      return;
    }
    this.userService.register(this.model).subscribe(result => {
      this.registerDone.emit();
    }, error => {
      console.log(error);
    });
  }

  cancelRegister() {
    this.registerDone.emit();
  }

}
