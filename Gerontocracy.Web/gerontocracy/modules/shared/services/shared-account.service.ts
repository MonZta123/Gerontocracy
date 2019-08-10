import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from '../../../models/user';

@Injectable({
  providedIn: 'root'
})
export class SharedAccountService {

  constructor(private httpClient: HttpClient) { }

  isLoggedIn(): Observable<boolean> {
    return this.httpClient.get<boolean>(`api/account/isloggedin`);
  }

  whoami(): Observable<User> {
    return this.httpClient.get<User>(`api/account/whoami`)
  }
}
