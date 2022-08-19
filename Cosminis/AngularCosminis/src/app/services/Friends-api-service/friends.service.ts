import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Friends } from '../../Models/Friends';

@Injectable({
  providedIn: 'root'
})
export class FriendsService {

  constructor(private http: HttpClient) { }
  url : string = environment.api;

  getAcceptedFriends(username : string) : Observable<Friends[]> {
    return this.http.get(this.url + `Friends/RelationshipStatusByUsername?username=${username}&status=Accepted`) as Observable<Friends[]>;  
  }

  addFriendByUsername(username : string, username2 : string) : Observable<Friends> {
    return this.http.post(this.url + `Friends/AddFriendByUsername?requesterUsername=${username}&addedUsername=${username2}`, username) as Observable<Friends>;
  } 

  EditFriendship(editingUserID : number, user2BeEdited : number, status : string) : Observable<Friends> {
    return this.http.put(this.url + `Friends/EditFriendshipStatus?editingUserID=${editingUserID}&user2BeEdited=${user2BeEdited}&status=${status}`, editingUserID) as Observable<Friends>;
  }

  FriendsByUserIds(searchingUserId : number, user2BeSearchedFor : number) : Observable<Friends> {
    return this.http.get(this.url + `Friends/FriendsByUserIds?searchingUserId=${searchingUserId}&user2BeSearched=${user2BeSearchedFor}`) as Observable<Friends>;  
  }  
}
