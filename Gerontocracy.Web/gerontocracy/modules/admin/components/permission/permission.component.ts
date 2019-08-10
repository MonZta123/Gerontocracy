import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MessageService } from 'Gerontocracy.Web/gerontocracy/modules/shared/services/message.service';
import { SearchParams } from '../../models/search-params';
import { UserOverview } from '../../models/user-overview';
import { MatSidenav } from '@angular/material';
import { AdminService } from '../../services/admin.service';
import { UserDetail } from '../../models/user-detail';

@Component({
  selector: 'app-permission',
  templateUrl: './permission.component.html',
  styleUrls: ['./permission.component.scss', '../../../../geronto-global-styles.scss']
})
export class PermissionComponent implements OnInit {
  @ViewChild('detailNav') detailNav: MatSidenav;

  constructor(
    private location: Location,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private adminService: AdminService
  ) { }

  searchForm: FormGroup;

  pageSize = 25;
  maxResults = 0;
  pageIndex = 0;
  searchParams: SearchParams;

  displayedColumns = ['id', 'userName', 'detailButton'];

  data: UserOverview[];
  detailData: UserDetail;

  isFullscreen = false;
  isLoadingData = false;

  ngOnInit() {
    this.searchForm = this.formBuilder.group({
      userName: ['']
    });

    this.activatedRoute.params.subscribe(n => {
      const id = +n.id;
      if (id) {
        this.showDetail(id);
      }
    });

    this.loadData(null);
  }

  showNav(evt: any, id: number) {
    this.location.replaceState(`admin/permission/${id}`);
    this.showDetail(id);
  }

  showDetail(id: number) {
    this.detailData = null;

    if (!this.detailNav.opened) {
      this.detailNav.open();
    }

    this.adminService.getUserDetail(id)
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

  loadData(evt: any): void {
    this.searchParams = this.mapToSearchParams(this.searchForm.value);
    this.pageIndex = 0;
    this.maxResults = 0;
    this.isLoadingData = true;
    this.adminService.search(this.searchParams, this.pageSize, this.pageIndex)
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
      userName: input.userName,
    } as SearchParams;

    return result;
  }
}
