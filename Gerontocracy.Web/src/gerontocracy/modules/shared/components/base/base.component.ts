import { Component, OnInit, Input } from '@angular/core';
import { MessageService } from 'primeng/api';
import { finalize } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { PostResult } from '../../models/post-result';
import { SharedService } from '../../services/shared.service';

@Component({
  selector: 'app-base',
  templateUrl: './base.component.html',
  styleUrls: ['./base.component.scss']
})
export class BaseComponent implements OnInit {

  constructor(
    private messageService: MessageService,
    public sharedService: SharedService) { }

  public loading: boolean;
  @Input() public uiBlocked: boolean;

  ngOnInit() {
    this.loading = false;
    this.uiBlocked = false;
  }

  protected blockUI() {
    this.uiBlocked = true;
  }

  protected start() {
    return <T>(source: Observable<T>) => {
      this.sharedService.loading = true;
      return source;
    };
  }

  protected end() {
    return <T>(source: Observable<T>) => source.pipe(finalize(() => {
      this.sharedService.loading = false;
    }));
  }

  protected handleError(message: string) {
    this.messageService.add({ severity: 'error', summary: 'Fehler', detail: message });
  }

  protected handlePostResult(data: PostResult<any>, successMessage?: string, successHeader?: string): boolean {
    if (data.errors) {
      this.messageService.addAll(data.errors.map(n =>
        ({
          severity: data.success ? 'warning' : 'error',
          closable: true,
          detail: n.message,
          summary: data.success ? 'Warnung' : 'Fehler'
        })));
    } else {
      if (successMessage) {
        this.messageService.add({
          severity: 'success',
          closable: true,
          detail: successMessage,
          summary: successHeader ? successHeader : 'Erledigt'
        });
      }
    }

    return data.success;
  }
}
