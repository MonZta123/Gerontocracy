import { Component, OnInit } from '@angular/core';
import { NewsService } from '../../services/news.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { MessageService, DialogService } from 'primeng/api';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Parliament } from '../../models/parliament';
import { AddSourceDialogComponent } from '../add-source-dialog/add-source-dialog.component';
import { AccountService } from 'Gerontocracy.Web/src/gerontocracy/services/account.service';
import { Router } from '@angular/router';
import { RssData } from '../../models/rss-data';

import { ConfirmationService } from 'primeng/api';
import { RssSource } from '../../models/rss-source';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss'],
  providers: [MessageService, DialogService]
})
export class SettingsComponent extends BaseComponent implements OnInit {

  constructor(
    private newsService: NewsService,
    private formBuilder: FormBuilder,
    private dialogService: DialogService,
    private accountService: AccountService,
    private router: Router,
    private confirmationService: ConfirmationService,
    messageService: MessageService) {
    super(messageService);
  }

  isAdmin: boolean;
  searchForm: FormGroup;

  pageSize = 25;
  maxResults = 0;
  pageIndex = 0;

  data: Parliament[];

  ngOnInit() {
    this.accountService
      .getCurrentUser()
      .toPromise()
      .then(n => {
        const roles: string[] = n.roles;
        if (roles.includes('admin') || roles.includes('moderator')) {
          this.isAdmin = true;
        } else {
          this.router.navigate(['~/']);
        }

        this.searchForm = this.formBuilder.group({
          search: ['']
        });

        this.loadData();
      });
  }

  addSource(parlamentId: number) {
    this.dialogService.open(AddSourceDialogComponent, {
      closable: false,
      header: 'Neue Source hinzufügen',
      width: '512px',
    }).onClose.subscribe(n => {
      if (n) {
        const data: RssData = {
          name: n.name,
          url: n.url,
          parlamentId
        };
        this.newsService.addRssFeed(data)
          .pipe(super.start(), super.end())
          .toPromise()
          .then(m => {

            if (super.handlePostResult(m, 'Source wurde hinzugefügt!', 'Erfolg')) {
              this.loadData();
            }
          })
          .catch(error => super.handleError(error));
      }
    });
  }

  editSource(id: number) {
    this.dialogService.open(AddSourceDialogComponent, {
      data: { id },
      closable: false,
      header: 'Neuen Vorfall einreichen',
      width: '512px',
    }).onClose.subscribe(n => {
      if (super.handlePostResult(n)) {
        this.loadData();
      }
    });
  }

  removeSource(source: RssSource) {
    this.confirmationService.confirm({
      message: `Sicher, dass die Source '${source.name}' entfernt werden soll?`,
      accept: () => {
        this.newsService.removeRssFeed(source.id).pipe(super.start(), super.end())
          .toPromise()
          .then(n => {
            if (super.handlePostResult(n, 'Eintrag wurde gelöscht!')) {
              this.loadData();
            }
          })
          .catch(error => super.handleError(error));
      }
    });
  }

  paginate(evt: any) {
    this.pageIndex = evt.page;
    this.pageSize = evt.rows;
    this.loadData();
  }

  loadData(): void {
    this.newsService.getAll(this.searchForm.controls.search.value, this.pageSize, this.pageIndex)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
      })
      .catch(error => super.handleError(error));
  }
}
