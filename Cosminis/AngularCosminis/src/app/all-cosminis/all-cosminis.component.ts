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

  cosminis1 : Cosminis = 
  {
    companionId : 1,
    trainerId : 1,
    userFk : 1,
    speciesFk : 1,
    nickname : "Shrek",
    emotion : 100,
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
      console.log(this.cosminis1);
      this.showCosminis=Promise.resolve(true);
    })
  }


showCards = false;
  ngOnInit(): void 
  {
    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    let currentUser : Users = JSON.parse(stringUser);
    let currentUserID : number = currentUser.userId as number;
    console.log(currentUserID);
    this.getCosminiByUserID(currentUserID);
  }
}
