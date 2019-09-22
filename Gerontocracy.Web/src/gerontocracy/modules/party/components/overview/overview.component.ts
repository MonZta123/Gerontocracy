import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { PolitikerOverview } from '../../models/politiker-overview';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PartyService } from '../../services/party.service';
import { MessageService } from 'primeng/api';
import { PolitikerDetail } from '../../models/politiker-detail';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { SharedService } from '../../../shared/services/shared.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss'],
  providers: [MessageService]
})
export class OverviewComponent extends BaseComponent implements OnInit {

  data: PolitikerOverview[];
  detailData: PolitikerDetail;
  searchForm: FormGroup;
  query: any;
  maxResults = 0;
  pageSize = 25;
  pageIndex = 0;
  popupVisible: boolean;

  constructor(
    private location: Location,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private partyService: PartyService,
    private router: Router,
    sharedService: SharedService,
    messageService: MessageService
  ) {
    super(messageService, sharedService);
  }

  ngOnInit() {
    this.popupVisible = false;
    this.pageIndex = 0;
    this.maxResults = 0;
    this.searchForm = this.formBuilder.group({
      name: [''],
      party: [''],
      includeInactive: [false]
    });

    this.activatedRoute.params.subscribe(n => {
      const id = +n.id;
      if (id) {
        this.showDetail(id);
      }
    });

    this.search();
  }

  showVorfall(id: number) {
    this.router.navigate([`affair/new/${id}`]);
  }

  showPopup(): void {
    this.popupVisible = true;
  }

  closePopup(): void {
    this.popupVisible = false;
  }

  search(): void {
    this.pageIndex = 0;
    this.query = this.searchForm.value;
    this.loadData();
  }

  loadData(): void {
    this.partyService.Search(this.query, this.pageSize, this.pageIndex)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
      })
      .catch(error => super.handleError(error));
  }

  showDetail(id: number) {
    this.detailData = null;

    this.location.replaceState(`party/${id}`);

    this.partyService.getPolitikerDetail(id)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => this.detailData = n)
      .catch(error => super.handleError(error));
  }

  paginate(evt: any) {
    this.pageIndex = evt.page;
    this.pageSize = evt.rows;
    this.loadData();
  }
}
