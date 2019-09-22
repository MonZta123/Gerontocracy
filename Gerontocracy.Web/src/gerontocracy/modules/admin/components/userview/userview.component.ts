import { Component, OnInit } from '@angular/core';
import { DialogService, MessageService } from 'primeng/api';
import { FormGroup, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { AdminService } from '../../services/admin.service';
import { UserOverview } from '../../models/user-overview';
import { UserDetail } from '../../models/user-detail';
import { AccountService } from '../../../../services/account.service';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { SharedService } from '../../../shared/services/shared.service';

@Component({
  selector: 'app-userview',
  templateUrl: './userview.component.html',
  styleUrls: ['./userview.component.scss'],
  providers: [DialogService]
})
export class UserviewComponent extends BaseComponent implements OnInit {

  searchForm: FormGroup;
  query: any;

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
    private adminService: AdminService,
    sharedService: SharedService,
    messageService: MessageService
  ) {
    super(messageService, sharedService);
  }

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

    this.search();
  }

  showDetail(id: number) {
    this.detailData = null;

    this.adminService.getUserDetail(id)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => this.detailData = n)
      .catch(error => super.handleError(error));
  }

  search(): void {
    this.pageIndex = 0;
    this.query = this.searchForm.value;
    this.loadData();
  }

  loadData(): void {
    this.pageIndex = 0;
    this.maxResults = 0;
    this.adminService.search(this.query, this.pageSize, this.pageIndex)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
      })
      .catch(error => super.handleError(error));
  }
}
