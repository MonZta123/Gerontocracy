import { Component, OnInit } from '@angular/core';
import { NewsService } from '../../services/news.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { MessageService, DialogService } from 'primeng/api';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Parliament } from '../../models/parliament';
import { AddSourceDialogComponent } from '../add-source-dialog/add-source-dialog.component';

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
    this.searchForm = this.formBuilder.group({
      search: ['']
    });

    this.loadData();
  }

  addSource() {
    this.dialogService.open(AddSourceDialogComponent, {
      closable: false,
      header: 'Neuen Vorfall einreichen',
      width: '800px',
    });
  }

  editSource(id: number) {
    this.dialogService.open(AddSourceDialogComponent, {
      data: { id },
      closable: false,
      header: 'Neuen Vorfall einreichen',
      width: '800px',
    }).onClose.subscribe(n => {
      if (super.handlePostResult(n)) {
        this.loadData();
      }
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
