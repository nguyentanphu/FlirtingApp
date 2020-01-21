import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { AuthGuard } from './_services/_guards/auth.guard';
import { ListComponent } from './lists/list/list.component';
import { MessageComponent } from './messages/message/message.component';
import { UserDetailResolver } from './_services/_resolvers/user-detail.resolver';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { PreventUnsavedChangesMemberEdit } from './_services/_guards/prevent-unsaved-changes-member-edit.guard';
import { MemberListResolver } from './_services/_resolvers/member-list.resolver';

export const appRoutes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: '',
    runGuardsAndResolvers: 'always',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'members',
        component: MemberListComponent,
        resolve: {
          users: MemberListResolver
        }
      },
      {
        path: 'members/edit',
        component: MemberEditComponent,
        resolve: {
          user: UserDetailResolver
        },
        canDeactivate: [PreventUnsavedChangesMemberEdit]
      },
      {
        path: 'list',
        component: ListComponent
      },
      {
        path: 'message',
        component: MessageComponent
      }
    ]
  }
];
