import { Component, OnInit } from '@angular/core';
import { MessageService, Message } from 'primeng/api';
import { Router, ActivatedRoute } from '@angular/router';
import { finalize } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { PostResult } from '../../models/post-result';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss'],
  providers: [MessageService]
})
export class BaseComponent implements OnInit {

  constructor(private messageService: MessageService) { }

  public loading: boolean;
  public uiBlocked: boolean;

  ngOnInit() {

  }

  protected blockUI() {
    this.uiBlocked = true;
  }

  protected start() {
    return <T>(source: Observable<T>) => {
      this.loading = true;
      return source;
    };
  }

  protected end() {
    return <T>(source: Observable<T>) => source.pipe(finalize(() => {
      this.loading = false;
    }));
  }

  protected handleError(handleError: HttpErrorResponse) {
    this.messageService.add({ severity: 'error', summary: 'Fehler', detail: handleError.message });
  }

  protected handlePostResult(data: PostResult<any>) {
    this.messageService.addAll(data.errors.map(n =>
      ({
        severity: data.success ? 'warning' : 'error',
        closable: true,
        detail: n.message,
        summary: data.success ? 'Warnung' : 'Fehler'
      })));
  }
}
