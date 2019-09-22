import { Injectable } from '@angular/core';
import { SearchParams } from '../models/search-params';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { ThreadDetail } from '../models/thread-detail';
import { LikeType } from '../models/like-type';
import { ThreadData } from '../models/thread-data';
import { ReportData } from '../models/report-data';
import { SearchResult } from '../../shared/models/search-result';
import { ThreadOverview } from '../models/thread-overview';
import { PostResult } from '../../shared/models/post-result';

@Injectable({
  providedIn: 'root'
})
export class BoardService {

  constructor(private httpClient: HttpClient) { }

  search(params: SearchParams, pageSize: number, pageIndex: number): Observable<SearchResult<ThreadOverview>> {
    const request = `api/board/threadSearch?`
      + `title=${params.title}&`
      + `pagesize=${pageSize}&`
      + `pageindex=${pageIndex}`;

    return this.httpClient.get<SearchResult<ThreadOverview>>(request);
  }

  getThread(id: number) {
    return this.httpClient.get<ThreadDetail>(`api/board/thread/${id}`);
  }

  like(postId: number, likeType: LikeType): Observable<PostResult<void>> {
    return this.httpClient.post<PostResult<void>>(`api/board/like`, { postId, likeType });
  }

  reply(postId: number, content: string): Observable<PostResult<void>> {
    return this.httpClient.post<PostResult<void>>(`api/board/reply`, { content, parentId: postId });
  }

  addThread(obj: ThreadData): Observable<PostResult<number>> {
    return this.httpClient.post<PostResult<number>>(`api/board/thread`, obj);
  }

  report(reportData: ReportData): Observable<PostResult<void>> {
    return this.httpClient.post<PostResult<void>>(`api/board/report`, reportData);
  }
}
