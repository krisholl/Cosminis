import { Component, OnInit} from '@angular/core';
import { AuthService } from '@auth0/auth0-angular';
import { ResourceApiServicesService } from '../services/Resource-Api-Service/resource-api-service.service';
import { FoodElement } from '../Models/FoodInventory';
import { Router } from '@angular/router';
import { Users } from '../Models/User';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})

export class NavbarComponent implements OnInit {

  constructor(public auth0: AuthService, private router: Router, private api:ResourceApiServicesService) { }
  foodDisplay : FoodElement[] = []

  foodQty : [number, number, number, number, number, number] = [0, 0, 0, 0, 0, 0];

  currentUsername : string ="";
  currentUsernickname : string ="";
  userGold:number = 0;
  userEgg:number = 0;
  SpicyFoodCount : number = 0;
  LeafyFoodCount : number = 0;
  ColdFoodCount : number = 0;
  FluffyFoodCount : number = 0;
  BlessedFoodCount : number = 0;
  CursedFoodCount : number = 0;
  DisplayCompanionMood : number = 0;
  DisplayCompanionHunger : number = 0;

  gotoHome(){
    this.router.navigateByUrl('/homepage');  // define your component where you want to go
  }
    
  ngOnInit(): void {
  }


  CheckFood():boolean
  {
    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    console.log(stringUser);
    let currentUser : Users = JSON.parse(stringUser);
    this.api.CheckFood(currentUser.userId as number).subscribe((res) =>
    {
      console.log(this.foodDisplay);
      this.foodDisplay= res;
      if(this.foodDisplay)
      {
        return true;
      }
      else
      {
        return false;
      }
    });
    return false;
  }

  Logout():void{
    this.auth0.logout()
    this.router.navigateByUrl('/login');
    console.log(this.currentUsername);
    sessionStorage.setItem("currentUser","");
  }

  Loggedin():boolean
  {  
    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    let currentUser : Users = JSON.parse(stringUser);
    this.currentUsername = currentUser.username;
    this.currentUsernickname = sessionStorage.getItem('currentUserNickname') as string;
    if(this.foodDisplay.length >= 1)
    {
      this.SpicyFoodCount = this.foodDisplay[0].amount;
      this.LeafyFoodCount = this.foodDisplay[1].amount;
      this.ColdFoodCount = this.foodDisplay[2].amount;
      this.FluffyFoodCount = this.foodDisplay[3].amount;
      this.BlessedFoodCount = this.foodDisplay[4].amount;
      this.CursedFoodCount = this.foodDisplay[5].amount;
    }
    else
    {
      this.SpicyFoodCount = 0;
      this.ColdFoodCount = 0;
      this.FluffyFoodCount = 0;
      this.BlessedFoodCount = 0;
      this.CursedFoodCount = 0;
      this.LeafyFoodCount = 0;
    }
    
    this.userEgg = currentUser.eggCount;
    this.userGold = currentUser.goldCount;
    if(this.currentUsername)
    {
      return true;
    }
    return false;
  }
}
