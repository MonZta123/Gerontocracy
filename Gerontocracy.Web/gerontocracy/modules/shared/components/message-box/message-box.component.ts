import { Component, OnInit, Input, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { MessageBoxParams } from '../../models/message-box-params';
import { IconType } from '../../models/icon-type';

@Component({
  selector: 'app-message-box',
  templateUrl: './message-box.component.html',
  styleUrls: ['./message-box.component.scss']
})
export class MessageBoxComponent implements OnInit {

  icon: string;

  constructor(
    public dialogRef: MatDialogRef<MessageBoxComponent>,
    @Inject(MAT_DIALOG_DATA) public data: MessageBoxParams) {

    if (!!data.icon) {
      switch (+data.icon) {
        case IconType.Error: this.icon = 'error'; break;
        case IconType.Info: this.icon = 'info'; break;
        case IconType.Warning: this.icon = 'warning'; break;
        case IconType.Question: this.icon = 'add_comment'; break;
      }
    }
  }

  ngOnInit(): void {

  }

  okClicked(evt: any) {
    this.dialogRef.close();
  }

}
