import { Component, OnInit } from '@angular/core';
import { AuthModule } from '@auth0/auth0-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  constructor(/*public auth: AuthService*/private router: Router) { }

  gotoHome(){
    this.router.navigateByUrl('/homepage');  // define your component where you want to go
  }

  ngOnInit(): void 
  {
    /*$('#login').click(async () => {
      await auth0.loginWithPopup();
    });*/
  }

}
