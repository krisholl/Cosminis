import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Component, OnDestroy, OnInit, EventEmitter, Output} from '@angular/core';
import { ComsinisApiServiceService } from '../services/Comsini-api-service/comsinis-api-service.service';
import { Cosminis } from '../Models/Cosminis';
import { Router } from '@angular/router';
import { Users } from '../Models/User';

@Component({
  selector: 'app-all-cosminis',
  templateUrl: './all-cosminis.component.html',
  styleUrls: ['./all-cosminis.component.css']
})
export class AllCosminisComponent implements OnInit 
{

  constructor(private api:ComsinisApiServiceService, private router: Router) { }
  showCosminis!:Promise<boolean>;
  cosminis : Cosminis[] = []
  DisplayName = new Map<number, string>();
  currentEmotion = new Map<number, string>();
  imageLib = new Map<number, string>();

  cosminis1 : Cosminis = 
  {
    companionId : 1,
    trainerId : 1,
    userFk : 1,
    speciesFk : 1,
    nickname : "Shrek",
    emotion : 100,
    mood : 100,
    hunger : 100
  }

  gotoHome(){
    this.router.navigateByUrl('/homepage');  // define your component where you want to go
  }

  updateCosminis() : void {
      this.api.getAllComsinis().subscribe((res) => 
      {
        console.log(res);
        this.cosminis = res;
        console.log(this.cosminis);
        this.showCosminis=Promise.resolve(true);
      })
  }

  getCosminiByID(ID : number) : void 
  {
    this.api.getCosminiByID(ID).subscribe((res) => 
    {
      console.log(res);
      this.cosminis1 = res;
      console.log(this.cosminis1);
      this.showCosminis=Promise.resolve(true);
    })
  }

  getCosminiByUserID(ID : number) : void 
  {
    this.api.getCosminiByUserID(ID).subscribe((res) => 
    {
      console.log(res);
      this.cosminis = res;

      for(let i=0;i<this.cosminis.length;i++)
      {
        this.cosminis[i].speciesNickname = this.DisplayName.get(this.cosminis[i].speciesFk);
        this.cosminis[i].emotionString = this.currentEmotion.get(this.cosminis[i].emotion);
        this.cosminis[i].image = this.imageLib.get(this.cosminis[i].speciesFk);
      }

      this.showCosminis=Promise.resolve(true);
    })
  }

showCards = false;
  ngOnInit(): void 
  {
    
    this.DisplayName.set(3, "Infernog");
    this.DisplayName.set(4, "Pluto");
    this.DisplayName.set(5, "Buds");
    this.DisplayName.set(6, "Cosmo");
    this.DisplayName.set(7, "Librian");
    this.DisplayName.set(8, "Cancer");

    this.imageLib.set(3, "InfernogFire.png");
    this.imageLib.set(4, "pluto.png");
    this.imageLib.set(5, "budfinal.png");
    this.imageLib.set(6, "cosmo.png");
    this.imageLib.set(7, "librianfinall.png");
    this.imageLib.set(8, "cancerfinal.png");

    this.currentEmotion.set(1, "Hopeless");
    this.currentEmotion.set(2, "hostile");
    this.currentEmotion.set(3, "Distant");
    this.currentEmotion.set(4, "Inadequate");
    this.currentEmotion.set(5, "Calm");
    this.currentEmotion.set(6, "Thankful");
    this.currentEmotion.set(7, "Happy");
    this.currentEmotion.set(8, "Playful");
    this.currentEmotion.set(9, "Inspired");
    this.currentEmotion.set(10, "Blissful");

    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    let currentUser : Users = JSON.parse(stringUser);
    let currentUserID : number = currentUser.userId as number;
    console.log(currentUserID);
    this.getCosminiByUserID(currentUserID);
  }
}
