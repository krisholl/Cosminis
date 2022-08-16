import { Component, OnInit } from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {

  constructor(public auth0: AuthService, private router: Router) { }

  gotoHome(){
    this.router.navigateByUrl('/homepage');  // define your component where you want to go
  }
    
  ngOnInit(): void {
  }

  Logout(){
    this.router.navigateByUrl('/login');
  }
  
}
