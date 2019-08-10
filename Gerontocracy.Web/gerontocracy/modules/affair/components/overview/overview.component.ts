import { Component, OnInit, ViewChild } from '@angular/core';
import { MessageService } from '../../../shared/services/message.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AffairService } from '../../services/affair.service';
import { PageEvent, MatSidenav } from '@angular/material';
import { VorfallOverview } from '../../models/vorfall-overview';
import { VorfallDetail } from '../../models/vorfall-detail';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { SearchParams } from '../../models/search-params';

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
    private affairService: AffairService,
    private messageService: MessageService) { }

  searchForm: FormGroup;

  pageSize = 25;
  maxResults = 0;
  pageIndex = 0;
  searchParams: SearchParams;

  displayedColumns = ['id', 'titel', 'reputation', 'politikerName', 'parteiName', 'erstelltAm', 'detailButton'];

  data: VorfallOverview[];
  detailData: VorfallDetail;

  isFullscreen = false;
  isLoadingData = false;

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      title: [''],
      lastName: [''],
      firstName: [''],
      party: [''],
      from: [''],
      to: [''],
      maxReputation: [100],
      minReputation: [-100]
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
    this.affairService.search(this.searchParams, this.pageSize, this.pageIndex)
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
        this.isLoadingData = false;
      })
      .catch(n => this.messageService.showSnackbar(n.message, null));
  }

  showNav(evt: any, id: number) {
    this.location.replaceState(`affair/top/${id}`);
    this.showDetail(id);
  }

  showDetail(id: number) {
    this.detailData = null;

    if (!this.detailNav.opened) {
      this.detailNav.open();
    }

    this.affairService.getAffairDetail(id)
      .toPromise()
      .then(n => this.detailData = n)
      .catch(n => this.messageService.showSnackbar(n.Message, null));
  }

  paginatorChanged(evt: PageEvent) {
    this.pageSize = evt.pageSize;
    this.pageIndex = evt.pageIndex;
    this.isLoadingData = true;

    this.affairService.search(this.searchParams, this.pageSize, this.pageIndex)
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
        this.isLoadingData = false;
      })
      .catch(n => this.messageService.showSnackbar(n.message, null));
  }

  hideNav(evt: any) {
    this.detailData = null;
    this.detailNav.close();
  }

  fullscreen(evt: boolean) {
    this.isFullscreen = evt;
  }

  private mapToSearchParams(input: any): SearchParams {
    const result = {
      from: (new Date(input.from).getTime() * 10000) + 621355968000000000 + 600000000 * 60 * 2,
      to: (new Date(input.to).getTime() * 10000) + 621355968000000000 + 600000000 * 60 * 2,
      maxReputation: input.maxReputation,
      minReputation: input.minReputation,
      nachname: input.lastName,
      parteiName: input.party,
      titel: input.title,
      vorname: input.firstName
    } as SearchParams;

    return result;
  }
}
