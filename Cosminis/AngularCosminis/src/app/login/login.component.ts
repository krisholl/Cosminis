import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(public auth0: AuthService, private router: Router) { }

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
