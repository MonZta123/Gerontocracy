import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { PolitikerOverview } from '../../models/politiker-overview';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';
import { PartyService } from '../../services/party.service';
import { MessageService } from 'primeng/api';
import { PolitikerDetail } from '../../models/politiker-detail';
import { BaseComponent } from '../../../shared/components/base/base.component';

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
  searchParams: any;
  maxResults = 0;
  pageSize = 25;
  pageIndex = 0;
  isLoadingData: boolean;
  popupVisible: boolean;

  constructor(
    private location: Location,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private partyService: PartyService,
    private router: Router,
    messageService: MessageService
  ) {
    super(messageService);
  }

  ngOnInit() {
    this.popupVisible = false;
    this.pageIndex = 0;
    this.maxResults = 0;
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

    this.loadData();
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

  loadData(): void {
    this.searchParams = this.searchForm.value;
    this.isLoadingData = true;
    this.partyService.Search(this.searchParams, this.pageSize, this.pageIndex)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
        this.isLoadingData = false;
      })
      .catch(super.handleError);
  }

  showDetail(id: number) {
    this.detailData = null;

    this.location.replaceState(`party/${id}`);

    this.partyService.getPolitikerDetail(id)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => this.detailData = n)
      .catch(this.handleError);
  }

  paginate(evt: any) {
    this.pageIndex = evt.page;
    this.pageSize = evt.rows;
    this.loadData();
  }
}
