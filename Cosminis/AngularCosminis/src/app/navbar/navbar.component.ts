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
      if(this.foodDisplay.length>0)
      {
        window.sessionStorage.setItem('SpicyFoodCount', this.foodDisplay[0].foodCount as unknown as string);
        window.sessionStorage.setItem('ColdFoodCount', this.foodDisplay[1].foodCount as unknown as string);
        window.sessionStorage.setItem('LeafyFoodCount', this.foodDisplay[2].foodCount as unknown as string);
        window.sessionStorage.setItem('FluffyFoodCount', this.foodDisplay[3].foodCount as unknown as string);
        window.sessionStorage.setItem('BlessedFoodCount', this.foodDisplay[4].foodCount as unknown as string);
        window.sessionStorage.setItem('CursedFoodCount', this.foodDisplay[5].foodCount as unknown as string);
        return true;
      }
      else
      {
        window.sessionStorage.setItem('SpicyFoodCount', '0');
        window.sessionStorage.setItem('ColdFoodCount', '0');
        window.sessionStorage.setItem('LeafyFoodCount', '0');
        window.sessionStorage.setItem('FluffyFoodCount', '0');
        window.sessionStorage.setItem('BlessedFoodCount', '0');
        window.sessionStorage.setItem('CursedFoodCount', '0');
        return false;
      }
    });
    return false;
  }

  Logout():void{
    this.auth0.logout()
    this.router.navigateByUrl('/login');
    console.log(this.currentUsername);
    sessionStorage.clear();
  }

  Loggedin():boolean
  {  
    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    let currentUser : Users = JSON.parse(stringUser);
    this.currentUsername = currentUser.username;
    this.currentUsernickname = sessionStorage.getItem('currentUserNickname') as string;
    
    this.SpicyFoodCount = sessionStorage.getItem('SpicyFoodCount') as unknown as number;
    this.LeafyFoodCount = sessionStorage.getItem('LeafyFoodCount') as unknown as number;
    this.ColdFoodCount = sessionStorage.getItem('ColdFoodCount') as unknown as number;
    this.FluffyFoodCount = sessionStorage.getItem('FluffyFoodCount') as unknown as number;
    this.BlessedFoodCount = sessionStorage.getItem('BlessedFoodCount') as unknown as number;
    this.CursedFoodCount = sessionStorage.getItem('CursedFoodCount') as unknown as number;
    
    this.userEgg = currentUser.eggCount;
    this.userGold = currentUser.goldCount;
    if(this.currentUsername)
    {
      return true;
    }
    return false;
  }
}
