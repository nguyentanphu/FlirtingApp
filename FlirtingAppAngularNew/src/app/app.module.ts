import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import * as MatModules from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { NavLoginComponent } from './nav/nav-login/nav-login.component';
import { HomeComponent } from './home/home.component';
import { RouterModule } from '@angular/router';
import { appRoutes } from './routes';
import { interceptorProviders } from './_services/_interceptors/interceptor-providers';
import { ErrorSnackbarComponent } from './notifications/error-snackbar/error-snackbar.component';
import { AppMenuComponent } from './nav/app-menu/app-menu.component';
import { NavLogoutComponent } from './nav/nav-logout/nav-logout.component';
import { RegistrationComponent } from './home/registration/registration.component';
import { MAT_DATE_LOCALE, MatNativeDateModule } from '@angular/material';
import { MemberListComponent } from './members/member-list/member-list.component';
import { ListComponent } from './lists/list/list.component';
import { MessageComponent } from './messages/message/message.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberEditIntroComponent } from './members/member-edit/member-edit-intro/member-edit-intro.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    NavLoginComponent,
    NavLogoutComponent,
    AppMenuComponent,
    ErrorSnackbarComponent,

    HomeComponent,
    RegistrationComponent,
    MemberListComponent,
    MemberEditComponent,
    MemberEditIntroComponent,
    ListComponent,
    MessageComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    ReactiveFormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes),
    FlexLayoutModule,

    MatModules.MatToolbarModule,
    MatModules.MatIconModule,
    MatModules.MatFormFieldModule,
    MatModules.MatInputModule,
    MatModules.MatButtonModule,
    MatModules.MatSelectModule,
    MatModules.MatSnackBarModule,
    MatModules.MatMenuModule,
    MatModules.MatDividerModule,
    MatModules.MatCardModule,
    MatModules.MatDatepickerModule,
    MatNativeDateModule,
    MatModules.MatTabsModule
  ],
  providers: [
    ...interceptorProviders,
    { provide: MAT_DATE_LOCALE, useValue: 'en-GB' }
  ],
  bootstrap: [AppComponent],
  entryComponents: [ErrorSnackbarComponent]
})
export class AppModule {}
