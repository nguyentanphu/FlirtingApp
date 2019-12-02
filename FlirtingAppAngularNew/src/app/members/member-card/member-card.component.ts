import { Component, OnInit, Input } from '@angular/core';
import { IUserOverview } from 'src/app/_models/users/user-overview';

@Component({
  selector: 'app-member-card',
  templateUrl: './member-card.component.html',
  styleUrls: ['./member-card.component.scss']
})
export class MemberCardComponent implements OnInit {
  @Input() user: IUserOverview;
  constructor() { }

  ngOnInit() {
  }

}
