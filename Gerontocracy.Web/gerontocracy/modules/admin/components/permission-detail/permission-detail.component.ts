import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { MatDialog } from '@angular/material';
import { UserDetail } from '../../models/user-detail';

@Component({
  selector: 'app-permission-detail',
  templateUrl: './permission-detail.component.html',
  styleUrls: ['./permission-detail.component.scss', '../../../../geronto-global-styles.scss']
})
export class PermissionDetailComponent implements OnInit {

  @Input() data: UserDetail;

  @Output() fullscreen: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() close: EventEmitter<any> = new EventEmitter<any>();

  isFullscreen: boolean;

  isLoading: boolean;

  constructor(
    private adminService: AdminService,
    private dialog: MatDialog
  ) { }

  ngOnInit() {
    this.isFullscreen = false;
  }

  toggleSize(evt: boolean) {
    this.isFullscreen = evt;
    this.fullscreen.emit(this.isFullscreen);
  }
}
