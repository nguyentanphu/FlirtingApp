import { AuthService } from './../../_services/auth.service';
import { UserService } from './../../_services/user.service';
import { AlertifyService } from './../../_services/alertify.service';
import { NgForm } from '@angular/forms';
import { Component, OnInit, ViewChild, HostListener } from '@angular/core';
import { User } from 'src/app/models/user';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.css']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('editForm', {static: true}) editForm: NgForm;
  user: User;
  @HostListener('window:beforeunload', ['$event'])
  beforeUnloadHandler($event: BeforeUnloadEvent) {
    if (this.editForm.dirty) {
      $event.returnValue = true;
    }
  }
  constructor(
    private route: ActivatedRoute,
    private alertifyService: AlertifyService,
    private userService: UserService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.user = data.user;
    });
  }

  updateUser() {
    const loggedInUserId = this.authService.decodedAccessToken.id;
    this.userService.updateUser(loggedInUserId, this.user).subscribe(() => {
      this.alertifyService.success('Profile edit successfully!');
      this.editForm.reset(this.user);
    }, (error) => {
      this.alertifyService.error(error);
    });
    
  }

}
