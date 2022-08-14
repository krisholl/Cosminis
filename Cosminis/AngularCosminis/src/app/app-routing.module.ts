import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomepageComponent } from './homepage/homepage.component';
import { LoginComponent } from './login/login.component';
import { UserprofileComponent } from './userprofile/userprofile.component';

const routes: Routes = [
  { 
    path: 'homepage', 
    component:HomepageComponent
  },  // you must add your component here
  {
    path: 'userprofile', 
    component:UserprofileComponent
  },
  {
    path: 'login', 
    component:LoginComponent
  },
  {
    path: '',
    redirectTo: '/login',
    pathMatch: 'full'
  },
  //{ path: '*', redirectTo: '/login', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
