import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { UserDetail } from '../../models/user-detail';
import { DialogService } from 'primeng/api';
import { AddRoleDialogComponent } from '../add-role-dialog/add-role-dialog.component';
import { AdminService } from '../../services/admin.service';
import { SharedAccountService } from '../../../shared/services/shared-account.service';
import { AccountService } from 'Gerontocracy.Web/src/gerontocracy/services/account.service';

@Component({
  selector: 'app-userdetail',
  templateUrl: './userdetail.component.html',
  styleUrls: ['./userdetail.component.scss'],
  providers: [DialogService]
})
export class UserdetailComponent implements OnInit {

  @Input() data: UserDetail;

  @Input() isPopup = false;
  @Output() popout: EventEmitter<void> = new EventEmitter<void>();

  isAdmin: boolean;

  constructor(
    private dialogService: DialogService,
    private adminService: AdminService,
    private accountService: AccountService) { }

  ngOnInit() {
    this.accountService.getCurrentUser()
      .toPromise()
      .then(n => this.isAdmin = n.roles.includes('admin'));
  }

  addRole() {
    this.adminService.getRoles().toPromise().then(allRoles => {
      const possibleRoles = allRoles.filter(n => !this.data.roles.find(m => m.id === n.id));

      this.dialogService.open(AddRoleDialogComponent, {
        closable: false,
        data: { possibleRoles }
      })
        .onClose
        .subscribe(n => {
          if (n) {
            this.adminService
              .grantRole({ userId: this.data.id, roleId: n.id })
              .toPromise()
              .then(m => this.data.roles.push(possibleRoles.find(o => o.id === n.id)));
          }
        });
    });
  }

  revokeRole(id: number) {
    this.adminService.revokeRole({ userId: this.data.id, roleId: id })
      .toPromise()
      .then(n => this.data.roles = this.data.roles.filter(m => m.id !== id));
  }
}
