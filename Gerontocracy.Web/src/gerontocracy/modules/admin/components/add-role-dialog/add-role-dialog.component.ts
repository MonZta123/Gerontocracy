import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, Message } from 'primeng/api';
import { Role } from '../../models/role';

@Component({
  selector: 'app-add-role-dialog',
  templateUrl: './add-role-dialog.component.html',
  styleUrls: ['./add-role-dialog.component.scss']
})
export class AddRoleDialogComponent implements OnInit {

  selectedRole: Role;

  msg: Message[];

  results: Role[];
  allRoles: Role[];

  constructor(
    private dialogReference: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig
  ) {
    this.allRoles = dialogConfig.data.possibleRoles;
    this.dialogConfig.header = 'User eine Rolle zuweisen';
    this.dialogConfig.width = '400px';
  }

  get rolesJoined() {
    return this.allRoles.map(n => n.name).join(', ');
  }

  ngOnInit() {
    this.msg = [];
  }

  search(evt: any) {
    this.results = this.allRoles.filter(n => n.name.includes(evt.query));
  }

  ok() {
    if (!this.selectedRole) {
      this.msg.push({ severity: 'error', summary: 'Fehler', detail: 'Es wurde keine Rolle ausgewählt!' });
    } else {
      this.dialogReference.close(this.selectedRole);
    }
  }

  unlockRole() {
    this.selectedRole = null;
  }

  close() {
    this.dialogReference.close();
  }
}
