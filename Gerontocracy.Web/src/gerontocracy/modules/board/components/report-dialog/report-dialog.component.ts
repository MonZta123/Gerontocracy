import { Component, OnInit } from '@angular/core';
import { DynamicDialogRef, DynamicDialogConfig, ConfirmationService } from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-report-dialog',
  templateUrl: './report-dialog.component.html',
  styleUrls: ['./report-dialog.component.scss']
})
export class ReportDialogComponent implements OnInit {

  formGroup: FormGroup;

  showConfirmation: boolean;

  constructor(
    private formBuilder: FormBuilder,
    private dialogReference: DynamicDialogRef,
    private config: DynamicDialogConfig,
    private confirmationService: ConfirmationService) { }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      comment: [null, Validators.maxLength(4000)]
    });
  }

  report() {
    console.log(this.formGroup.valid);
    if (this.formGroup.valid) {
      this.confirmationService.confirm({
        message: 'Sicher, dass du diesen Beitrag melden möchtest?',
        accept: () => {
          this.close(this.formGroup.controls.comment.value);
        }
      });
    }
  }

  close(data: any) {
    this.dialogReference.close(data);
  }
}
