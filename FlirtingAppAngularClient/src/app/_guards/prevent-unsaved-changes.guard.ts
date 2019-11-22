import { MemberEditComponent } from './../members/member-edit/member-edit.component';
import { Injectable } from '@angular/core';
import { CanDeactivate, RouterStateSnapshot, ActivatedRouteSnapshot } from '@angular/router';

@Injectable({
    providedIn: 'root'
})
export class PreventUnsavedChanges implements CanDeactivate<MemberEditComponent> {
    canDeactivate(component: MemberEditComponent) {
        if (component.editForm.dirty) {
            return confirm('Unsaved changes may be lost');
        }

        return true;
    }
}