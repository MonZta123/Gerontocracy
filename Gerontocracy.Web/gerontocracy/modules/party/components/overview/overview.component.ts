import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { PolitikerOverview } from '../../models/politiker-overview';
import { PartyService } from '../../services/party.service';
import { MessageService } from '../../../shared/services/message.service';
import { PageEvent, MatSidenav } from '@angular/material';
import { PolitikerDetail } from '../../models/politiker-detail';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

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
    private partyService: PartyService,
    private messageService: MessageService
  ) { }

  searchForm: FormGroup;

  pageSize = 25;
  maxResults = 0;
  pageIndex = 0;
  searchParams: any;
  displayedColumns = ['id', 'externalId', 'vorname', 'nachname', 'titel', 'bundesland', 'score', 'detailButton'];

  data: PolitikerOverview[];
  detailData: PolitikerDetail;
  dataLength = 0;

  isFullscreen = false;
  isLoadingData = false;

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      lastName: [''],
      firstName: [''],
      party: [''],
    });

    this.activatedRoute.params.subscribe(n => {
      const id = +n.id;
      if (id) {
        this.showDetail(id);
      }
    });
  }

  loadData(evt: any): void {
    this.searchParams = this.searchForm.value;
    this.pageIndex = 0;
    this.maxResults = 0;
    this.isLoadingData = true;
    this.partyService.Search(this.searchParams, this.pageSize, this.pageIndex)
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
        this.isLoadingData = false;
      })
      .catch(n => this.messageService.showSnackbar(n.message, null));
  }

  showDetail(id: number) {
    this.detailData = null;

    if (!this.detailNav.opened) {
      this.detailNav.open();
    }

    this.partyService.getPolitikerDetail(id)
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

  showNav(evt: any, id: number) {
    this.location.replaceState(`party/${id}`);
    this.showDetail(id);
  }

  paginatorChanged(evt: PageEvent) {
    this.pageSize = evt.pageSize;
    this.pageIndex = evt.pageIndex;
    this.isLoadingData = true;
    this.partyService.Search(this.searchParams, this.pageSize, this.pageIndex)
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
        this.isLoadingData = false;
      })
      .catch(n => this.messageService.showSnackbar(n.message, null))
      .then(() => this.isLoadingData = false);
  }
}
