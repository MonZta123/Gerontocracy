import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class SharedService {

  public loading: boolean;

  constructor() {
    this.loading = false;
  }
}
