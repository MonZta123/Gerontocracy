import { Component, OnInit, ViewChild } from '@angular/core';
import { MatSidenav, PageEvent } from '@angular/material';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { MessageService } from 'Gerontocracy.Web/gerontocracy/modules/shared/services/message.service';
import { BoardService } from '../../services/board.service';
import { Location } from '@angular/common';
import { ThreadOverview } from '../../models/thread-overview';
import { SearchParams } from '../../models/search-params';
import { ThreadDetail } from '../../models/thread-detail';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss', '../../../../geronto-global-styles.scss']
})
export class OverviewComponent implements OnInit {
  @ViewChild('detailNav') detailNav: MatSidenav;

  constructor(
    private location: Location,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private boardService: BoardService,
    private messageService: MessageService) {
  }

  pageSize = 25;
  maxResults = 0;
  pageIndex = 0;
  searchParams: SearchParams;

  searchForm: FormGroup;

  displayedColumns = ['id', 'numPosts', 'titel', 'politiker', 'createdOn', 'createdBy', 'generated', 'detailButton'];

  data: ThreadOverview[];
  detailData: ThreadDetail;

  isFullscreen = false;
  isLoadingData = false;

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      title: [''],
    });

    this.activatedRoute.params.subscribe(n => {
      const id = +n.id;
      if (id) {
        this.showDetail(id);
      }
    });

    this.loadData(null);
  }

  loadData(evt: any): void {
    this.searchParams = this.mapToSearchParams(this.searchForm.value);
    this.pageIndex = 0;
    this.maxResults = 0;
    this.isLoadingData = true;
    this.boardService.search(this.searchParams, this.pageSize, this.pageIndex)
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
        this.isLoadingData = false;
      })
      .catch(n => this.messageService.showSnackbar(n.message, null));
  }

  showNav(evt: any, id: number) {
    this.location.replaceState(`board/top/${id}`);
    this.showDetail(id);
  }

  showDetail(id: number) {
    this.detailData = null;

    if (!this.detailNav.opened) {
      this.detailNav.open();
    }

    this.boardService.getThread(id)
      .toPromise()
      .then(n => this.detailData = n)
      .catch(n => this.messageService.showSnackbar(n.Message, null));
  }

  hideNav(evt: any) {
    this.detailData = null;
    this.detailNav.close();
  }

  fullscreen(evt: boolean) {
    this.isFullscreen = evt;
  }

  paginatorChanged(evt: PageEvent) {
    this.pageSize = evt.pageSize;
    this.pageIndex = evt.pageIndex;
    this.isLoadingData = true;

    this.boardService.search(this.searchParams, this.pageSize, this.pageIndex)
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
        this.isLoadingData = false;
      })
      .catch(n => this.messageService.showSnackbar(n.message, null));
  }

  private mapToSearchParams(input: any): SearchParams {
    const result = {
      titel: input.title,
    } as SearchParams;

    return result;
  }
}
