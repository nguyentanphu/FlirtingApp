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
import { interceptorProviders } from './_services/interceptors/interceptor-providers';
import { environment } from 'src/environments/environment';
import { ErrorSnackbarComponent } from './notifications/error-snackbar/error-snackbar.component';
import { AppMenuComponent } from './nav/app-menu/app-menu.component';
import { NavLogoutComponent } from './nav/nav-logout/nav-logout.component';
import { RegistrationComponent } from './home/registration/registration.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      NavLoginComponent,
      NavLogoutComponent,
      AppMenuComponent,
      ErrorSnackbarComponent,
      
      HomeComponent,
      RegistrationComponent
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
      MatModules.MatSnackBarModule,
      MatModules.MatMenuModule
   ],
   providers: [
     ...interceptorProviders,
   ],
   bootstrap: [
      AppComponent
   ],
   entryComponents: [
    ErrorSnackbarComponent
   ]
})
export class AppModule { }
