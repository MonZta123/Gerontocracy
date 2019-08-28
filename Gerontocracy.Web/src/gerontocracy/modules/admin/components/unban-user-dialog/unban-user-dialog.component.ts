import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogRef } from 'primeng/api';

@Component({
  selector: 'app-unban-user-dialog',
  templateUrl: './unban-user-dialog.component.html',
  styleUrls: ['./unban-user-dialog.component.scss']
})
export class UnbanUserDialogComponent implements OnInit {

  formGroup: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private dialogReference: DynamicDialogRef) { }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      reason: null
    });
  }

  confirmUnban() {
    this.close(this.formGroup.value);
  }

  cancel() {
    this.close(null);
  }

  close(value: any) {
    this.dialogReference.close(value);
  }
}
