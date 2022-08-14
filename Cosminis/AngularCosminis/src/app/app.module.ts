import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { HomepageComponent } from './homepage/homepage.component';
import { UserprofileComponent } from './userprofile/userprofile.component';
import { AuthModule } from '@auth0/auth0-angular';
import { environment as env } from '../environments/environment';
//import { AppLoginComponent } from './login/app-login/app-login.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomepageComponent,
    UserprofileComponent
    //AppLoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    /*AuthModule.forRoot
    ({
      ... env.auth,
    })*/
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
