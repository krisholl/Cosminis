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
}
