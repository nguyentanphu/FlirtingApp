import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUserOverview } from 'src/app/_models/users/user-overview';
import { UserService } from 'src/app/_services/user-service';
import { IMemberListFilter } from 'src/app/_models/users/member-list-filter';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {
  users: IUserOverview[] = [];
  constructor(private userService: UserService) { }

  ngOnInit() {
    const filter: IMemberListFilter = { distance: 10000 };
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position: Position) => {
        filter.coordinates = [position.coords.longitude, position.coords.latitude];
        this.userService.getUsersOverview(filter).subscribe(users => {
          this.users = users;
        });
      }, () => {
        this.userService.getUsersOverview(filter).subscribe(users => {
          this.users = users;
        });
      });

    } else {
      this.userService.getUsersOverview(filter).subscribe(users => {
        this.users = users;
      });
    }
  }

}
