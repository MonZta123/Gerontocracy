import { Component, OnInit, EventEmitter, Input, Output } from '@angular/core';
import { UserDetail } from '../../models/user-detail';

@Component({
  selector: 'app-userdetail',
  templateUrl: './userdetail.component.html',
  styleUrls: ['./userdetail.component.scss']
})
export class UserdetailComponent implements OnInit {

  @Input() data: UserDetail;

  @Input() isPopup = false;
  @Output() popout: EventEmitter<void> = new EventEmitter<void>();

  constructor() { }

  ngOnInit() {
  }

  joinText(roles: string[]) {
    return roles.join(', ');
  }

}
