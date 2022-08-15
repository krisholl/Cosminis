import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {

  constructor(private router: Router) { }

  gotoHome(){
    this.router.navigateByUrl('/homepage');  // define your component where you want to go
  }

  ngOnInit(): void {
  }

}
