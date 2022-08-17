import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Router } from '@angular/router';
import { Users } from '../Models/User';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public auth0: AuthService, private router: Router) { }

  currentUser : Users = 
  {
    username : 'DefaultUserName',
    password: "NoOneIsGoingToSeeThis",
    account_age : new Date(),
    eggTimer : new Date(),
    goldCount : 1,
    eggCount : 1,
    showcaseCompanion_fk:1,
    aboutMe:"I am Boring",
  }

  gotoHome(){
    this.router.navigateByUrl('/homepage');  // define your component where you want to go
  }

  loggedIn: boolean = false;

  ngOnInit(): void 
  {
    this.auth0.user$.subscribe((userInfo) => 
    {
      console.log(userInfo);
      //this.user = userInfo;
    
      if(userInfo)
        //window.sessionStorage.setItem('currentUserId', JSON.parse(userInfo))
        this.gotoHome();
    }) 
  }

}
