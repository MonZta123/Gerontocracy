import { Component, OnInit, Inject } from '@angular/core';
import { YesNoBoxParams } from '../../models/yes-no-box-params';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';
import { IconType } from '../../models/icon-type';

@Component({
  selector: 'app-yes-no-box',
  templateUrl: './yes-no-box.component.html',
  styleUrls: ['./yes-no-box.component.scss']
})
export class YesNoBoxComponent implements OnInit {

  icon: string;

  constructor(
    public dialogRef: MatDialogRef<YesNoBoxComponent>,
    @Inject(MAT_DIALOG_DATA) public data: YesNoBoxParams) {

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
}
