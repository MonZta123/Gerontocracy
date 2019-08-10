import { Injectable } from '@angular/core';
import { PolitikerOverview } from '../models/politiker-overview';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { SearchResult } from '../models/search-result';
import { PolitikerDetail } from '../models/politiker-detail';

@Injectable({
  providedIn: 'root'
})
export class PartyService {

  constructor(private httpClient: HttpClient) { }

  public Search(params: any, pageSize: number, pageIndex: number): Observable<SearchResult> {
    const request = `api/party/parteisearch?`
      + `lastname=${params.lastName}&`
      + `firstname=${params.firstName}&`
      + `party=${params.party}&`
      + `includenotactive=${params.includeNotActive}&`
      + `pagesize=${pageSize}&`
      + `pageindex=${pageIndex}`;

    return this.httpClient.get<SearchResult>(request);
  }

  public getPolitikerDetail(id: number): Observable<PolitikerDetail> {
    return this.httpClient.get<PolitikerDetail>(`api/party/politikerdetail/${id}`);
  }
}
