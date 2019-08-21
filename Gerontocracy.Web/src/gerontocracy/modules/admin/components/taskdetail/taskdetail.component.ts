import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DialogService } from 'primeng/api';
import { AdminService } from '../../services/admin.service';
import { AccountService } from '../../../../services/account.service';
import { AufgabeDetail } from '../../models/aufgabe-detail';

@Component({
  selector: 'app-taskdetail',
  templateUrl: './taskdetail.component.html',
  styleUrls: ['./taskdetail.component.scss']
})
export class TaskdetailComponent implements OnInit {

  @Input() data: AufgabeDetail;

  @Input() isPopup = false;
  @Output() popout: EventEmitter<void> = new EventEmitter<void>();

  isAdmin: boolean;

  constructor(
    private dialogService: DialogService,
    private adminService: AdminService,
    private accountService: AccountService) {
  }

  ngOnInit() {
    this.isAdmin = false;
    this.accountService.getCurrentUser()
      .toPromise()
      .then(n => this.isAdmin = n.roles.includes('admin'));
  }

  assignToMe() {
    this.adminService.assignTask(this.data.id).toPromise().then(n => {
      this.data.bearbeiter = n.userName;
      this.data.bearbeiterId = n.id;
    });
  }

  closeTask() {
    this.adminService.closeTask(this.data.id).toPromise().then(n => this.data.erledigt = n);
  }

  reopenTask() {
    this.adminService.reopenTask(this.data.id).toPromise().then(n => this.data.erledigt = n);
  }
}
