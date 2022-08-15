import { MatDialog, MatDialogRef } from '@angular/material/dialog';
import { Component, OnDestroy, OnInit, EventEmitter, Output} from '@angular/core';
import { ComsinisApiServiceService } from '../services/Comsini-api-service/comsinis-api-service.service';
import { Cosminis } from '../Models/Cosminis';

@Component({
  selector: 'app-all-cosminis',
  templateUrl: './all-cosminis.component.html',
  styleUrls: ['./all-cosminis.component.css']
})
export class AllCosminisComponent implements OnInit {

  constructor(private api:ComsinisApiServiceService) { }
  cosminis : Cosminis[] = 
  [{
    CompanionId : 1,
    TrainerId : 1,
    UserFk : 1,
    SpeciesFk : 1,
    Nickname : "Shrek",
    Emotion : 100,
    Hunger : 100
  }]

  cosminis1 : Cosminis = 
  {
    CompanionId : 1,
    TrainerId : 1,
    UserFk : 1,
    SpeciesFk : 1,
    Nickname : "Shrek",
    Emotion : 100,
    Hunger : 100
  }

  updateCosminis() : void {
      this.api.getAllComsinis().subscribe((res) => 
      {
        console.log(res);
        this.cosminis = res;
        console.log(this.cosminis);
      })
  }

  getCosminiByID(ID : number) : void {
    this.api.getCosminiByID(ID).subscribe((res) => 
    {
      console.log(res);
      this.cosminis1 = res;
      console.log(this.cosminis1);
    })
}

  ngOnInit(): void {
  }

}
