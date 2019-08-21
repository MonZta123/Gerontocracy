import { Component, OnInit } from '@angular/core';
import { DialogService, MessageService } from 'primeng/api';
import { Location } from '@angular/common';
import { FormGroup, FormBuilder } from '@angular/forms';
import { SearchParams } from '../../../affair/models/search-params';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../../services/admin.service';
import { UserOverview } from '../../models/user-overview';
import { UserDetail } from '../../models/user-detail';
import { AccountService } from '../../../../services/account.service';

@Component({
  selector: 'app-userview',
  templateUrl: './userview.component.html',
  styleUrls: ['./userview.component.scss'],
  providers: [DialogService]
})
export class UserviewComponent implements OnInit {

  searchForm: FormGroup;

  popupVisible: boolean;

  isAdmin: boolean;

  pageSize = 25;
  maxResults = 0;
  pageIndex = 0;

  data: UserOverview[];
  detailData: UserDetail;

  constructor(
    private router: Router,
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private adminService: AdminService
  ) { }

  ngOnInit() {
    this.isAdmin = false;
    this.accountService
      .getCurrentUser()
      .toPromise()
      .then(n => {
        const roles: string[] = n.roles;
        if (roles.includes('admin')) {
          this.isAdmin = true;
        } else {
          this.router.navigate(['/']);
        }
      });

    this.popupVisible = false;

    this.searchForm = this.formBuilder.group({
      userName: ['']
    });

    this.activatedRoute.params.subscribe(n => {
      const id = +n.id;
      if (id) {
        this.showDetail(id);
      }
    });

    this.loadData();
  }

  showDetail(id: number) {
    this.detailData = null;

    this.adminService.getUserDetail(id)
      .toPromise()
      .then(n => this.detailData = n)
      .catch(n => this.messageService.add({ severity: 'error', detail: n.Message, summary: 'Fehler' }));
  }

  loadData(): void {
    this.pageIndex = 0;
    this.maxResults = 0;
    this.adminService.search(this.searchForm.value, this.pageSize, this.pageIndex)
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
      })
      .catch(n => this.messageService.add({ severity: 'error', detail: n.Message, summary: 'Fehler' }));
  }
}
