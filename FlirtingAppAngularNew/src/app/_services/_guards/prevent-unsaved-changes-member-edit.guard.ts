import { CanDeactivate, ActivatedRouteSnapshot } from '@angular/router';
import { MemberEditComponent } from 'src/app/members/member-edit/member-edit.component';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class PreventUnsavedChangesMemberEdit implements CanDeactivate<MemberEditComponent> {
  canDeactivate(component: MemberEditComponent): boolean {
    if (component.memberEditIntro.editIntroFormGroup.dirty) {
      return confirm('Unsaved changes may be lost');
    }

    return true;
  }
}
