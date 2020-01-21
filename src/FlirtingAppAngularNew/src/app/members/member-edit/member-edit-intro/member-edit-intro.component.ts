import { Component, OnInit, Input, HostListener } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { IUserDetail } from 'src/app/_models/users/user-detail';
import { UserService } from 'src/app/_services/user-service';
import { NotificationService } from 'src/app/_services/notification-service';
import { IUserAdditionalModel } from 'src/app/_models/users/user-additional-model';

@Component({
  selector: 'app-member-edit-intro',
  templateUrl: './member-edit-intro.component.html',
  styleUrls: ['./member-edit-intro.component.scss']
})
export class MemberEditIntroComponent implements OnInit {
  @Input() user: IUserDetail;
  editIntroFormGroup: FormGroup;

  @HostListener('window:beforeunload', ['$event'])
  beforeUnloadHandler($event: BeforeUnloadEvent) {
    if (this.editIntroFormGroup.dirty) {
      $event.returnValue = true;
    }
  }

  constructor(
    private formBuilder: FormBuilder, 
    private userService: UserService,
    private notiService: NotificationService
  ) {
    this.editIntroFormGroup = this.formBuilder.group({
      introduction: [
        '',
        Validators.compose([Validators.required, Validators.minLength(20)])
      ],
      lookingFor: [
        '',
        Validators.compose([Validators.required, Validators.minLength(20)])
      ],
      interests: [
        '',
        Validators.compose([Validators.required, Validators.minLength(20)])
      ],
      city: [
        '',
        Validators.compose([Validators.required, Validators.minLength(3)])
      ],
      country: [
        '',
        Validators.compose([Validators.required, Validators.minLength(5)])
      ]
    });
  }

  ngOnInit() {
    const currentUser = this.user;
    this.editIntroFormGroup.patchValue({
      introduction: currentUser.introduction,
      lookingFor: currentUser.lookingFor,
      interests: currentUser.interests,
      city: currentUser.city,
      country: currentUser.country
    });
  }

  submitAdditionalUserDetail() {
    if (!this.editIntroFormGroup.valid) {
      return;
    }

    const updateModel = this.editIntroFormGroup.value as IUserAdditionalModel;
    this.userService.updateAdditionalDetail(updateModel).subscribe(() => {
      this.notiService.sendSuccess('Update user additional detail succeeded.');
      this.editIntroFormGroup.reset(updateModel);
    });
  }
}
