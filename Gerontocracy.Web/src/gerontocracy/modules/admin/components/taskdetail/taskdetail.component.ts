import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { DialogService, MessageService } from 'primeng/api';
import { AdminService } from '../../services/admin.service';
import { AccountService } from '../../../../services/account.service';
import { AufgabeDetail } from '../../models/aufgabe-detail';
import { BaseComponent } from '../../../shared/components/base/base.component';

@Component({
  selector: 'app-taskdetail',
  templateUrl: './taskdetail.component.html',
  styleUrls: ['./taskdetail.component.scss'],
  providers: [MessageService]
})
export class TaskdetailComponent extends BaseComponent implements OnInit {

  @Input() data: AufgabeDetail;

  @Input() isPopup = false;
  @Output() popout: EventEmitter<void> = new EventEmitter<void>();

  isAdmin: boolean;

  constructor(
    private adminService: AdminService,
    private accountService: AccountService,
    messageService: MessageService) {
    super(messageService);
  }

  ngOnInit() {
    this.isAdmin = false;
    this.accountService.getCurrentUser()
      .toPromise()
      .then(n => this.isAdmin = n.roles.includes('admin'));
  }

  assignToMe() {
    this.adminService.assignTask(this.data.id)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        super.handlePostResult(n);
        this.data.bearbeiter = n.data.userName;
        this.data.bearbeiterId = n.data.id;
      })
      .catch(super.handleError);
  }

  closeTask() {
    this.adminService.closeTask(this.data.id)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        super.handlePostResult(n);
        this.data.erledigt = true;
      })
      .catch(super.handleError);
  }

  reopenTask() {
    this.adminService.reopenTask(this.data.id)
      .pipe(super.start(), super.end())
      .toPromise()
      .then(n => {
        super.handlePostResult(n);
        this.data.erledigt = false;
      })
      .catch(super.handleError);
  }
}
