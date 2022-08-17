import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Users } from '../../Models/User';

@Injectable({
  providedIn: 'root'
})
export class UserApiServicesService {
  url : string = environment.api;

  constructor(private http: HttpClient) { }

  LoginOrReggi(User : Users) : Observable<Users> {
    return this.http.post(this.url + `Users/LoggiORReggi`, User) as Observable<Users>;
  } 
}
