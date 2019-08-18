import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SearchParams } from '../models/search-params';
import { SearchResult } from '../models/search-result';
import { Observable } from 'rxjs';
import { UserDetail } from '../models/user-detail';
import { UserRole } from '../models/user-role';
import { Role } from '../models/role';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(
    private httpClient: HttpClient
  ) { }

  search(params: SearchParams, pageSize: number, pageIndex: number): Observable<SearchResult> {
    const request = `api/admin/usersearch?`
      + `username=${params.userName}&`
      + `pagesize=${pageSize}&`
      + `pageindex=${pageIndex}`;

    return this.httpClient.get<SearchResult>(request);
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
}
