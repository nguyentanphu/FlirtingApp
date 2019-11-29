import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { AuthGuard } from './_services/guards/auth.guard';
import { ListComponent } from './lists/list/list.component';
import { MessageComponent } from './messages/message/message.component';

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
        component: MemberListComponent
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
