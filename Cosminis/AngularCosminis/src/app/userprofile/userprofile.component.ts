import { Component, OnInit } from '@angular/core';
import { PostSpiServicesService } from '../services/Post-api-services/post-spi-services.service';
import { UserApiServicesService } from '../services/User-Api-Service/user-api-services.service';
import { FriendsService } from '../services/Friends-api-service/friends.service';
import { Posts } from '../Models/Posts';
import { Users } from '../Models/User';
import { Router } from '@angular/router';
import { Friends } from '../Models/Friends';

@Component({
  selector: 'app-userprofile',
  templateUrl: './userprofile.component.html',
  styleUrls: ['./userprofile.component.css']
})
export class UserprofileComponent implements OnInit {

  constructor(private api:PostSpiServicesService, private router: Router, private userApi:UserApiServicesService, private friendApi:FriendsService) { }

  friendshipInstance : Friends =
  {
    userIdFrom : 1,
    userIdTo: 1,
    status: 'updatedStatus',
  }

  doesExist : boolean = false;
  successfulAdd : boolean = false;

  userInstance : Users =
  {
    username : 'DefaultUserName',
    userId : 1,
    password: "NoOneIsGoingToSeeThis",
    account_age : new Date(),
    eggTimer : new Date(),
    goldCount : 1,
    eggCount : 1,
    showcaseCompanion_fk:1,
    showcaseCompanionFk:1,
    aboutMe:"I am Boring... zzzz snoringgg",    
  }

  posts : Posts[] = []

  postInstance : Posts =
  {
    postId : 1,
    userIdFk : 1,
    content : "Shrek",    
  }

  friends : Friends[] = []
  users : Users[] = []

  inputValue : string = "";

  gotoHome()
  {
    this.router.navigateByUrl('/homepage');  // define your component where you want to go
  }

  enterUserId()
  {
    this.inputValue = (document.querySelector('.form-control') as HTMLInputElement).value;
    let inputNumber = parseInt(this.inputValue);
    this.updatePostFeed(inputNumber);
    
    console.log(this.inputValue);
  }

  searchAndAdd(username2 : string)
  {
    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    let searchingUser = JSON.parse(stringUser);
    
    this.searchUsers(username2);

    this.friendApi.addFriendByUsername(searchingUser.username, this.userInstance.username).subscribe((res) => 
    {
      this.friendshipInstance = res;

      console.log(this.friendshipInstance);

      if(this.friendshipInstance.status == 'Pending')
      {
        console.log(res);
        alert("Friend request sent!");
        console.log(alert);
      }
    })
  }

  searchUsers(searchedUser : string) : void
  {
    this.userApi.searchFriend(searchedUser).subscribe((res) =>
    {
      this.userInstance = res;
      console.log(this.userInstance);

      if(this.userInstance.username != 'DefaultUserName')
      {
        this.doesExist = true;
      }
    })
  }

  searchingFriendship(searchedUser : string) : void
  {
    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    let searchingUser = JSON.parse(stringUser);
    
    this.searchUsers(searchedUser);

    this.friendApi.FriendsByUserIds(searchingUser.userId, this.userInstance.userId as number).subscribe((res) =>
    {
      console.log(res);
    })
  }  

  updatePostFeed(ID : number) : void 
  {
    this.api.getPostsByUserId(ID).subscribe((res) => 
    {
      console.log(res);
      this.posts = res;
      console.log(this.postInstance);
    })
  }

  friendsPostFeed(username : string) : void 
  {
    this.api.getAllFriendsPosts(username).subscribe((res) => 
    {
      this.posts = res;
      let postUser:Users;
      let userID:number;
      for(let i =0; i<this.posts.length;i++)
      {
        userID = this.posts[i].userIdFk;
        this.userApi.Find(userID).subscribe((res) =>
        {
          postUser = res;
          console.log(postUser);
          this.posts[i].posterNickname = postUser.password;
        })
      }
    })
  }

  showAllFriends(username:string):void
  {
    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    let currentUser : Users = JSON.parse(stringUser);
    let currentID = currentUser.userId;
    this.friendApi.getAcceptedFriends(username).subscribe((res) => 
    {
      this.friends = res; //this just retrieves a list of friends for now, doing the relevant logic on ngOnInit
      for(let i=0;i<this.friends.length;i++)
      {
        if(currentID==this.friends[i].userIdFrom)
        {
          this.userApi.Find(this.friends[i].userIdTo).subscribe((res) =>
          {
            this.users[i] = res;
            console.log(this.users[i].password);
          })
        }
        else
        {
          this.userApi.Find(this.friends[i].userIdFrom).subscribe((res) =>
          {
            this.users[i] = res;
            console.log(this.users[i].password);
          })
        }
      }
    })
  }

  ngOnInit(): void 
  {
    let stringUser : string = sessionStorage.getItem('currentUser') as string;
    let currentUser : Users = JSON.parse(stringUser);
    let currentUsername = currentUser.username;
    this.friendsPostFeed(currentUsername);
    this.showAllFriends(currentUsername);
  }
}
