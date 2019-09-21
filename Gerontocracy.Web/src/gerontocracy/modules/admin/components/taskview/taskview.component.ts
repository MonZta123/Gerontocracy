import { Component, OnInit } from '@angular/core';
import { MessageService, SelectItem } from 'primeng/api';
import { Router, ActivatedRoute, ResolveStart } from '@angular/router';
import { AccountService } from '../../../../services/account.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { AufgabeOverview } from '../../models/aufgabe-overview';
import { AufgabeDetail } from '../../models/aufgabe-detail';
import { BaseComponent } from '../../../shared/components/base/base.component';

@Component({
  selector: 'app-taskview',
  templateUrl: './taskview.component.html',
  styleUrls: ['./taskview.component.scss']
})
export class TaskviewComponent extends BaseComponent implements OnInit {

  isAdmin: boolean;

  searchForm: FormGroup;
  query: any;

  includeDone: boolean;

  popupVisible: boolean;

  pageSize = 25;
  maxResults = 0;
  pageIndex = 0;

  selectionItems: SelectItem[];

  data: AufgabeOverview[];
  detailData: AufgabeDetail;

  constructor(
    private router: Router,
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private formBuilder: FormBuilder,
    private adminService: AdminService,
    messageService: MessageService) {
    super(messageService);
  }

  ngOnInit() {
    this.isAdmin = false;

    this.selectionItems = [
      {
        value: 0,
        label: 'Vorfallsmeldung'
      },
      {
        value: 1,
        label: 'Postmeldung'
      },
      {
        value: 2,
        label: 'Usermeldung'
      },
      {
        value: 3,
        label: 'Vorfall <-> Thread'
      }
    ];

    this.accountService
      .getCurrentUser()
      .toPromise()
      .then(n => {
        const roles: string[] = n.roles;
        if (roles.includes('admin') || roles.includes('moderator')) {
          this.isAdmin = true;
        } else {
          this.router.navigate(['~/']);
        }
        this.popupVisible = false;

        this.searchForm = this.formBuilder.group({
          userName: '',
          includeDone: false,
          taskType: null,
        });

        this.activatedRoute.params.subscribe(m => {
          const id = +m.id;
          if (id) {
            this.showDetail(id);
          }
        });

        this.loadData();
      });
  }

  showDetail(id: number) {
    this.detailData = null;

    this.adminService.getTaskDetail(id)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => this.detailData = n)
      .catch(error => super.handleError(error));
  }

  search(): void {
    this.pageIndex = 0;

    this.query = this.searchForm.value;
    if (!this.query.taskType && this.query.taskType !== 0) {
      this.query.taskType = '';
    }

    this.loadData();
  }

  loadData(): void {
    this.pageIndex = 0;
    this.maxResults = 0;

    this.adminService.getTasks(this.query, this.pageSize, this.pageIndex)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
      })
      .catch(error => super.handleError(error));
  }
}
