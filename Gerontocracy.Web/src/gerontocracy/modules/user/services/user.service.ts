import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User } from '../models/user';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  public getUserByName(name: string): Observable<User> {
    return this.httpClient.get<User>(`api/user/${name}`);
  }

  public getUserByClaims(): Observable<User> {
    return this.httpClient.get<User>(`api/user`);
  }

  public getUserById(id: number): Observable<User> {
    return this.httpClient.get<User>(`api/user/${id}`);
  }
}
