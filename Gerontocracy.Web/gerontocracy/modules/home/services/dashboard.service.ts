import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { DashboardData } from '../models/dashboard-data';
import { Observable } from 'rxjs';
import { NewsData } from '../models/news-data';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private httpClient: HttpClient) { }

  public getDashboardData(): Observable<DashboardData> {
    return this.httpClient.get<DashboardData>('api/dashboard');
  }

  public generateNews(data: NewsData): Observable<number> {
    return this.httpClient.post<number>('api/dashboard/generate', data);
  }
}
