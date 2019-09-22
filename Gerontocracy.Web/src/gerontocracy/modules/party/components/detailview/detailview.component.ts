import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PolitikerDetail } from '../../models/politiker-detail';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { MessageService } from 'primeng/api';
import { SharedService } from '../../../shared/services/shared.service';

@Component({
  selector: 'app-detailview',
  templateUrl: './detailview.component.html',
  styleUrls: ['./detailview.component.scss'],
  providers: [MessageService]
})
export class DetailviewComponent extends BaseComponent implements OnInit {

  @Input() data: PolitikerDetail;

  @Input() isPopup = false;
  @Output() popout: EventEmitter<void> = new EventEmitter<void>();
  @Output() vorfallClicked: EventEmitter<number> = new EventEmitter<number>();

  constructor(
    sharedService: SharedService,
    messageService: MessageService) {
    super(messageService, sharedService);
  }

  ngOnInit() {
  }

}
