import { Injectable } from '@angular/core';
import { SearchParams } from '../models/search-params';
import { UserOverview } from '../models/user-overview';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SearchResult } from '../models/search-result';
import { UserDetail } from '../models/user-detail';

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
}
