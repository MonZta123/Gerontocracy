import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchResult } from '../models/search-result';
import { Observable } from 'rxjs';
import { UserDetail } from '../models/user-detail';
import { UserRole } from '../models/user-role';
import { Role } from '../models/role';
import { UserOverview } from '../models/user-overview';
import { AufgabeOverview } from '../models/aufgabe-overview';
import { AufgabeDetail } from '../models/aufgabe-detail';
import { User } from '../../../models/user';
import { BanData } from '../models/ban-data';
import { UnbanData } from '../models/unban-data';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(
    private httpClient: HttpClient
  ) { }

  search(params: any, pageSize: number, pageIndex: number): Observable<SearchResult<UserOverview>> {
    const request = `api/admin/search-users?`
      + `username=${params.userName}&`
      + `pagesize=${pageSize}&`
      + `pageindex=${pageIndex}`;

    return this.httpClient.get<SearchResult<UserOverview>>(request);
  }

  getTasks(params: any, pageSize: number, pageIndex: number): Observable<SearchResult<AufgabeOverview>> {
    const request = `api/admin/search-tasks?`
      + `username=${params.userName}&`
      + `taskType=${params.taskType}&`
      + `includeDone=${params.includeDone}&`
      + `pagesize=${pageSize}&`
      + `pageindex=${pageIndex}`;

    return this.httpClient.get<SearchResult<AufgabeOverview>>(request);
  }

  getTaskDetail(id: number) {
    return this.httpClient.get<AufgabeDetail>(`api/admin/task/${id}`);
  }

  getUserDetail(id: number) {
    return this.httpClient.get<UserDetail>(`api/admin/user/${id}`);
  }

  grantRole(data: UserRole): Observable<void> {
    return this.httpClient.post<void>(`api/admin/grant-role`, data);
  }

  revokeRole(data: UserRole): Observable<void> {
    return this.httpClient.post<void>(`api/admin/revoke-role`, data);
  }

  getRoles(): Observable<Role[]> {
    return this.httpClient.get<Role[]>(`api/admin/roles`);
  }

  getTask(id: number): Observable<AufgabeDetail> {
    return this.httpClient.get<AufgabeDetail>(`api/admin/task/${id}`);
  }

  assignTask(id: number): Observable<User> {
    return this.httpClient.post<User>(`api/admin/task/assign`, id);
  }

  closeTask(id: number): Observable<boolean> {
    return this.httpClient.post<boolean>(`api/admin/task/close`, id);
  }

  reopenTask(id: number): Observable<boolean> {
    return this.httpClient.post<boolean>(`api/admin/task/reopen`, id);
  }

  deletePost(id: number): Observable<void> {
    return this.httpClient.delete<void>(`api/admin/post/${id}`);
  }

  deleteThread(id: number): Observable<void> {
    return this.httpClient.delete<void>(`api/admin/thread/${id}`);
  }

  banUser(banData: BanData): Observable<Date> {
    return this.httpClient.post<Date>(`api/admin/ban`, banData);
  }

  unbanUser(unbanData: UnbanData): Observable<void> {
    return this.httpClient.post<void>(`api/admin/unban`, unbanData);
  }
}
