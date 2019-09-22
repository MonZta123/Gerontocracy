import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { VorfallDetail } from '../models/vorfall-detail';
import { VorfallAdd } from '../models/vorfall-add';
import { SearchParams } from '../models/search-params';
import { VoteType } from '../models/vote-type';
import { VorfallOverview } from '../models/vorfall-overview';
import { SearchResult } from '../../shared/models/search-result';
import { PostResult } from '../../shared/models/post-result';

@Injectable({
  providedIn: 'root'
})
export class AffairService {

  constructor(private httpClient: HttpClient) { }

  public search(params: SearchParams, pageSize: number, pageIndex: number): Observable<SearchResult<VorfallOverview>> {
    const request = `api/affair/affairsearch?`
      + `title=${params.title}&`
      + `lastname=${params.lastName}&`
      + `firstname=${params.firstName}&`
      + `party=${params.party}&`
      + `pagesize=${pageSize}&`
      + `pageindex=${pageIndex}`;

    return this.httpClient.get<SearchResult<VorfallOverview>>(request);
  }

  public getAffairDetail(id: number): Observable<VorfallDetail> {
    return this.httpClient.get<VorfallDetail>(`api/affair/vorfalldetail/${id}`);
  }

  public vote(vorfallId: number, voteType: VoteType): Observable<PostResult<void>> {
    return this.httpClient.post<PostResult<void>>(`api/affair/vote`, { vorfallId, voteType });
  }

  public addAffair(obj: VorfallAdd): Observable<PostResult<number>> {
    return this.httpClient.post<PostResult<number>>(`api/affair/vorfall`, obj);
  }
}
