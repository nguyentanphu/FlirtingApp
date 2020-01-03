import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IUserOverview } from 'src/app/_models/users/user-overview';
import { UserService } from 'src/app/_services/user-service';
import { IMemberListFilter } from 'src/app/_models/users/member-list-filter';
import { Gender } from 'src/app/_models/users/sign-up-model';
import { enumToKeyValueArray } from 'src/app/_services/helpers/enum-helpers';
import { FormBuilder, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-member-list',
  templateUrl: './member-list.component.html',
  styleUrls: ['./member-list.component.scss']
})
export class MemberListComponent implements OnInit {
  filterFormGroup: FormGroup;
  geolocationAllowed = true;
  constructor(
    private userService: UserService,
    private formBuilder: FormBuilder
  ) {
    this.filterFormGroup = this.formBuilder.group({
      distance: [5000],
      gender: [null]
    });
  }
  users: IUserOverview[] = [];

  genderOptions = enumToKeyValueArray<number>(Gender);

  formatLabel(value: number) {
    if (value >= 1000) {
      return Math.round(value / 1000) + " k";
    }

    return value;
  }
  ngOnInit() {
    this.submitFilter();
  }

  submitFilter() {
    const filter: IMemberListFilter = this.filterFormGroup.value;
    navigator.geolocation.getCurrentPosition(
      (position: Position) => {
        filter.coordinates = [
          position.coords.longitude,
          position.coords.latitude
        ];
        this.updateMemberList(filter);
      },
      () => {
        this.geolocationAllowed = false;
        this.updateMemberList(filter);
      }
    );
  }

  private updateMemberList(filter: IMemberListFilter) {
    this.userService.getUsersOverview(filter).subscribe(users => {
      this.users = users;
    });
  }
}
