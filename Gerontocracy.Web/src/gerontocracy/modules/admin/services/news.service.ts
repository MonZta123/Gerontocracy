import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { PostResult } from '../../shared/models/post-result';
import { RssData } from '../models/rss-data';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NewsService {

  constructor(private httpClient: HttpClient) { }

  public addRssFeed(data: RssData): Observable<PostResult<void>> {
    return this.httpClient.post<PostResult<void>>(`api/news/rss`, data);
  }

  public removeRssFeed(id: number): Observable<PostResult<void>> {
    return this.httpClient.delete<PostResult<void>>(`api/news/rss/${id}`);
  }

  public getAll(search: string, pageSize: number, pageIndex: number): Observable<void> {
    return this.httpClient.get<void>(`api/news/rss?search=${search}&pageSize=${pageSize}&pageIndex=${pageIndex}`);
  }
}
