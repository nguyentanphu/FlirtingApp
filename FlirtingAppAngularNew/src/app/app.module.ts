import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import * as MatModules from '@angular/material';
import { FlexLayoutModule } from '@angular/flex-layout';

import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { NavLoginComponent } from './nav/nav-login/nav-login.component';
import { NotificationComponent } from './notification/notification.component';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      NavLoginComponent,
      NotificationComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      ReactiveFormsModule,
      FlexLayoutModule,
      MatModules.MatToolbarModule,
      MatModules.MatIconModule,
      MatModules.MatFormFieldModule,
      MatModules.MatInputModule,
      MatModules.MatButtonModule,
      MatModules.MatSnackBarModule,
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
