import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  smellingHandler:Function = () =>
  {
    let audio = new Audio();
    audio.src = "../assets/Audio/1.mp3";
    audio.load();
    audio.play();
  }

  GoAway:Function = () =>
  {
    this.router.navigateByUrl('/Go');  // define your component where you want to go
  }

  GoBabies:Function = () =>
  {
    this.router.navigateByUrl('/MyBabies');  // define your component where you want to go
  }

  gotoUserProfile(){
    this.router.navigateByUrl('/userprofile');  // define your component where you want to go
  }
}
