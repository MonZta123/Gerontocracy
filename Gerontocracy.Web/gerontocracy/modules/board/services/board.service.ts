import { Injectable } from '@angular/core';
import { SearchParams } from '../models/search-params';
import { Observable } from 'rxjs';
import { SearchResult } from '../models/search-result';
import { HttpClient } from '@angular/common/http';
import { ThreadDetail } from '../models/thread-detail';
import { LikeType } from '../models/like-type';
import { ThreadData } from '../models/thread-data';

@Injectable({
  providedIn: 'root'
})
export class BoardService {

  constructor(private httpClient: HttpClient) { }

  search(params: SearchParams, pageSize: number, pageIndex: number): Observable<SearchResult> {
    const request = `api/board/threadSearch?`
      + `title=${params.titel}&`
      + `pagesize=${pageSize}&`
      + `pageindex=${pageIndex}`;

    return this.httpClient.get<SearchResult>(request);
  }

  getThread(id: number) {
    return this.httpClient.get<ThreadDetail>(`api/board/thread/${id}`);
  }

  like(postId: number, likeType: LikeType): Observable<void> {
    return this.httpClient.post<void>(`api/board/like`, { postId, likeType });
  }

  reply(postId: number, content: string): Observable<void> {
    return this.httpClient.post<void>(`api/board/reply`, { content, parentId: postId });
  }

  addThread(obj: ThreadData): Observable<number> {
    return this.httpClient.post<number>(`api/board/thread`, obj);
  }
}
