import { Component, OnInit } from '@angular/core';
import { ComsinisApiServiceService } from '../services/Comsini-api-service/comsinis-api-service.service';
import { Posts } from '../Models/Posts';
import { Router } from '@angular/router';

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {

  constructor(private api:ComsinisApiServiceService, private router: Router) { }

  showPosts!:Promise<boolean>;
  posts : Posts[] = []

  postInstance : Posts =
  {
    postId : 1,
    userIdFk : 1,
    content : "Shrek",    
  }

  inputValue : string = "";

  gotoHome(){
    this.router.navigateByUrl('/homepage');  // define your component where you want to go
  }

  enterUserId(){
    /*let pot = document.querySelector('#userId');
    let input = pot.value;*/
    
    this.inputValue = (document.querySelector('.form-control') as HTMLInputElement).value;
    let inputNumber = parseInt(this.inputValue);
    this.updatePostFeed(inputNumber);
    
    console.log(inputNumber);
  }

  updatePostFeed(ID : number) : void 
  {
    this.api.getPostsByUserId(ID).subscribe((res) => 
    {
      console.log(res);
      this.posts = res;
      console.log(this.postInstance);
      this.showPosts=Promise.resolve(true);
    })
  }

  ngOnInit(): void 
  {
    //this.updatePostFeed(this.inputValue as unknown as number);
  }

}
