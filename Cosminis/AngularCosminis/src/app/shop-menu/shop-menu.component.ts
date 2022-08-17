import { Component, OnInit } from '@angular/core';
import { ResourceApiServicesService } from '../services/Resource-Api-Service/resource-api-service.service';
import { UserApiServicesService } from '../services/User-Api-Service/user-api-services.service';
import { Router } from '@angular/router';
import { Users } from '../Models/User';
import { FoodElement } from '../Models/FoodInventory';

@Component({
  selector: 'app-shop-menu',
  templateUrl: './shop-menu.component.html',
  styleUrls: ['./shop-menu.component.css']
})
export class ShopMenuComponent implements OnInit {

  constructor(private router: Router, private api:ResourceApiServicesService) { }
  foodInvInstance : FoodElement[] = []
  eggQty : number = 0;
  foodQty : [number, number, number, number, number, number] = [0, 0, 0, 0, 0, 0];
  purchaseTotal : number = 0;

  confirmPurchase() : void {
    let currentUserId = sessionStorage.getItem("currentUserId") as unknown as number;
    //this.foodQty;
    //this.eggQty;
    console.log(this.foodQty);
     this.api.Purchase(currentUserId, this.foodQty, this.eggQty).subscribe((res) => 
     {
       console.log(res);
       this.foodInvInstance = res;
       console.log(this.foodInvInstance);
       //this.showCosminis=Promise.resolve(true);
     })
}

updateTotal() : void {
  this.purchaseTotal = this.eggQty * 100;
  for(let i = 0; i < this.foodQty.length; i++) {
    this.purchaseTotal += this.foodQty[i] * 10;
  }
}

  ngOnInit(): void {
  }

}
