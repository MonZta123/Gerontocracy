import { Component, OnInit } from '@angular/core';
import { MessageService, SelectItem } from 'primeng/api';
import { Router, ActivatedRoute, ResolveStart } from '@angular/router';
import { AccountService } from '../../../../services/account.service';
import { FormBuilder, FormGroup } from '@angular/forms';
import { AdminService } from '../../services/admin.service';
import { AufgabeOverview } from '../../models/aufgabe-overview';
import { AufgabeDetail } from '../../models/aufgabe-detail';

@Component({
  selector: 'app-taskview',
  templateUrl: './taskview.component.html',
  styleUrls: ['./taskview.component.scss']
})
export class TaskviewComponent implements OnInit {

  isAdmin: boolean;

  searchForm: FormGroup;
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
    private messageService: MessageService,
    private adminService: AdminService
  ) { }

  ngOnInit() {
    this.isAdmin = false;
    this.includeDone = false;

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
          this.router.navigate(['/']);
        }
        this.popupVisible = false;

        this.searchForm = this.formBuilder.group({
          userName: '',
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

  checkboxChanged(value: boolean) {
    this.includeDone = value;
  }

  showDetail(id: number) {
    this.detailData = null;

    this.adminService.getTaskDetail(id)
      .toPromise()
      .then(n => this.detailData = n)
      .catch(n => this.messageService.add({ severity: 'error', detail: n.Message, summary: 'Fehler' }));
  }

  loadData(): void {
    this.pageIndex = 0;
    this.maxResults = 0;

    const searchParameters = this.searchForm.value;
    if (!searchParameters.taskType && searchParameters.taskType !== 0) {
      searchParameters.taskType = '';
    }
    searchParameters.includeDone = this.includeDone;

    this.adminService.getTasks(searchParameters, this.pageSize, this.pageIndex)
      .toPromise()
      .then(n => {
        this.data = n.data;
        this.maxResults = n.maxResults;
      })
      .catch(n => this.messageService.add({ severity: 'error', detail: n.Message, summary: 'Fehler' }));
  }
}
