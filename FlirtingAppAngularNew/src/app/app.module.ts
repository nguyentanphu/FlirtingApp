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
import { JwtModule } from '@auth0/angular-jwt';
import { accessTokenGetter } from './_services/auth-service';
import { environment } from 'src/environments/environment';

@NgModule({
   declarations: [
      AppComponent,
      NavComponent,
      NavLoginComponent,
      HomeComponent
   ],
   imports: [
      BrowserModule,
      BrowserAnimationsModule,
      ReactiveFormsModule,
      HttpClientModule,
      RouterModule.forRoot(appRoutes),
      JwtModule.forRoot({
         config: {
            tokenGetter: accessTokenGetter,
            whitelistedDomains: [environment.appDomain],
            blacklistedRoutes: [`${environment.appDomain}/auth/login`]
         }
      }),

      FlexLayoutModule,

      MatModules.MatToolbarModule,
      MatModules.MatIconModule,
      MatModules.MatFormFieldModule,
      MatModules.MatInputModule,
      MatModules.MatButtonModule,
      MatModules.MatSnackBarModule
   ],
   providers: [
     ...interceptorProviders,
   ],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
