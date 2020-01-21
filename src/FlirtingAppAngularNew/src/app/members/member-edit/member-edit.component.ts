import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUserDetail } from 'src/app/_models/users/user-detail';
import { MemberEditIntroComponent } from './member-edit-intro/member-edit-intro.component';

@Component({
  selector: 'app-member-edit',
  templateUrl: './member-edit.component.html',
  styleUrls: ['./member-edit.component.scss']
})
export class MemberEditComponent implements OnInit {
  @ViewChild('memberEditIntro', { static: true })
  memberEditIntro: MemberEditIntroComponent;

  user: IUserDetail;
  constructor(private route: ActivatedRoute) { }

  ngOnInit() {
    this.user = this.route.snapshot.data.user as IUserDetail;
  }

}
