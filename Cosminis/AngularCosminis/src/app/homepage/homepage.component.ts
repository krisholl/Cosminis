import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  smellingHandler:Function = () =>
  {
    let audio = new Audio();
    audio.src = "../assets/Audio/1.mp3";
    audio.load();
    audio.play();
  }
}
