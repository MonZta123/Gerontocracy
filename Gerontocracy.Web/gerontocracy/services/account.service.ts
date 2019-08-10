import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable } from 'rxjs';

import { Register } from '../models/register';
import { Login } from '../models/login';
import { EmailConfirmation } from '../models/emailconfirmation';
import { Result } from '../models/result';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    private httpClient: HttpClient
  ) { }

  getUserExists(user: string): Observable<boolean> {
    return this.httpClient.get<boolean>(`api/account/userexists/${user}`);
  }

  getEmailExists(email: string): Observable<boolean> {
    return this.httpClient.get<boolean>(`api/account/emailexists/${email}/mail`);
  }

  registerUser(data: Register): Observable<void> {
    return this.httpClient.post<void>(`api/account/register`, data);
  }

  loginUser(data: Login): Observable<void> {
    return this.httpClient.post<void>(`api/account/login`, data);
  }

  logoutUser(): Observable<void> {
    return this.httpClient.post<void>(`api/account/logout`, null);
  }

  confirmEmail(data: EmailConfirmation): Observable<Result> {
    return this.httpClient.post<Result>(`api/account/confirmemail`,
      { id: data.id, token: replaceAll(replaceAll(data.token, '%20', '+'), ' ', '+') });
  }

  getCurrentUser(): Observable<User> {
    return this.httpClient.get<User>(`api/account/whoami`);
  }

  isLoggedIn(): Observable<boolean> {
    return this.httpClient.get<boolean>(`api/account/isloggedin`);
  }
}

function replaceAll(str: string, find: string, replace: string) {
  return str.replace(new RegExp(escapeRegExp(find), 'g'), replace);
}

function escapeRegExp(str) {
  return str.replace(/([.*+?^=!:${}()|\[\]\/\\])/g, '\\$1');
}
