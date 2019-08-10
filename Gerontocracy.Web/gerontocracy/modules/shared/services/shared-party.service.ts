import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PolitikerSelection } from '../models/politiker-selection';

@Injectable({
  providedIn: 'root'
})
export class SharedPartyService {

  constructor(private httpClient: HttpClient) { }

  getPoliticianSelection(filter: string): Observable<PolitikerSelection[]> {
    return this.httpClient.get<PolitikerSelection[]>(`/api/party/politiker-selection?search=${filter}`);
  }
}
