import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { PolitikerDetail } from '../models/politiker-detail';
import { PolitikerOverview } from '../models/politiker-overview';
import { SearchResult } from '../../shared/models/search-result';

@Injectable({
  providedIn: 'root'
})
export class PartyService {

  constructor(private httpClient: HttpClient) { }

  public Search(params: any, pageSize: number, pageIndex: number): Observable<SearchResult<PolitikerOverview>> {
    const request = `api/party/parteisearch?`
      + `lastname=${params.lastName}&`
      + `firstname=${params.firstName}&`
      + `party=${params.party}&`
      + `includenotactive=${params.includeNotActive}&`
      + `pagesize=${pageSize}&`
      + `pageindex=${pageIndex}`;

    return this.httpClient.get<SearchResult<PolitikerOverview>>(request);
  }

  public getPolitikerDetail(id: number): Observable<PolitikerDetail> {
    return this.httpClient.get<PolitikerDetail>(`api/party/politikerdetail/${id}`);
  }
}
