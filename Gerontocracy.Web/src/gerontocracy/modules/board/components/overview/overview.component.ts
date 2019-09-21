import { Component, OnInit } from '@angular/core';
import { MessageService, DialogService, DynamicDialogConfig } from 'primeng/api';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Location } from '@angular/common';
import { BoardService } from '../../services/board.service';
import { ThreadOverview } from '../../models/thread-overview';
import { ThreadDetail } from '../../models/thread-detail';
import { SharedAccountService } from '../../../shared/services/shared-account.service';
import { LoginDialogComponent } from 'Gerontocracy.Web/src/gerontocracy/components/login-dialog/login-dialog.component';
import { AddDialogComponent } from '../add-dialog/add-dialog.component';
import { BaseComponent } from '../../../shared/components/base/base.component';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss'],
  providers: [DialogService, MessageService]
})
export class OverviewComponent extends BaseComponent implements OnInit {

  constructor(
    private location: Location,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private boardService: BoardService,
    private sharedAccountService: SharedAccountService,
    private dialogService: DialogService,
    messageService: MessageService
  ) {
    super(messageService);
  }

  pageSize = 25;
  maxResults = 0;
  pageIndex = 0;

  data: ThreadOverview[];
  detailData: ThreadDetail;
  isAdmin: boolean;

  isLoadingData: boolean;

  popupVisible: boolean;

  searchForm: FormGroup;
  query: any;

  ngOnInit() {
    this.isAdmin = false;
    this.sharedAccountService.whoami().toPromise().then(n => {
      if (n && n.roles && (n.roles.includes('admin') || n.roles.includes('moderator'))) {
        this.isAdmin = true;
      }
    });
    this.popupVisible = false;

    this.searchForm = this.formBuilder.group({
      title: [''],
    });

    this.activatedRoute.params.subscribe(n => {
      const id = +n.id;
      if (id) {
        this.showDetail(id);
      }
    });

    this.loadData();
  }

  paginate(evt: any) {
    this.pageIndex = evt.page;
    this.pageSize = evt.rows;
    this.loadData();
  }

  showPopup(): void {
    this.popupVisible = true;
  }

  addThread(): void {
    this.sharedAccountService.isLoggedIn().toPromise().then(r => {
      if (r) {
        this.dialogService.open(AddDialogComponent, {
          closable: false,
          header: 'Neuen Thread öffnen',
          width: '800px',
        }).onClose
          .subscribe(n => {
            if (n) {
              window.location.reload();
            }
          });
      } else {
        this.dialogService.open(LoginDialogComponent,
          {
            header: 'Login',
            width: '407px',
            closable: false,
          })
          .onClose
          .subscribe(m => {
            if (m) {
              window.location.reload();
            }
          });
      }
    });
  }

  search(): void {
    this.pageIndex = 0;
    this.query = this.searchForm.value;
    this.loadData();
  }

  loadData(): void {
    this.isLoadingData = true;
    this.boardService.search(this.query, this.pageSize, this.pageIndex)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
        this.isLoadingData = false;
      })
      .catch(error => super.handleError(error));
  }

  showDetail(id: number) {
    this.detailData = null;

    this.location.replaceState(`board/new/${id}`);

    this.boardService.getThread(id)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => this.detailData = n)
      .catch(error => super.handleError(error));
  }

  getThreadTitle(row: ThreadOverview): string {
    let result = row.titel;

    if (row.vorfallTitel !== row.titel) {
      result = `${result}/${row.vorfallTitel}`;
    }

    return result;
  }
}
