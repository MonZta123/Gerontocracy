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

            if (super.handlePostResult(m)) {
              this.loadData();
            }
          })
          .catch(super.handleError);
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
      // if (super.handlePostResult(n)) {
      //   this.loadData();
      // }
    });
  }

  removeSource(id: number) {
    this.newsService.removeRssFeed(id).pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        super.handlePostResult(n);
      })
      .catch(super.handleError);
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
      .catch(super.handleError);
  }
}
