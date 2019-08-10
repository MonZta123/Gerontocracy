import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { VorfallSelection } from '../models/vorfall-selection';

@Injectable({
  providedIn: 'root'
})
export class SharedBoardService {

  constructor(private httpClient: HttpClient) { }

  getAffairSelection(filter: string): Observable<VorfallSelection[]> {
    return this.httpClient.get<VorfallSelection[]>(`/api/board/affair-selection?search=${filter}`);
  }
}
