import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';
import { SourceDialogData } from './source-dialog-data';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { QuelleAdd } from '../../models/quelle-add';

@Component({
  selector: 'app-source-dialog',
  templateUrl: './source-dialog.component.html',
  styleUrls: ['./source-dialog.component.scss']
})
export class SourceDialogComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<SourceDialogComponent>,
    private formBuilder: FormBuilder,
    @Inject(MAT_DIALOG_DATA) public data: SourceDialogData,
  ) { }

  formGroup: FormGroup;

  private index: number;
  private readonly regex = '((https?://)|(http?://))([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';

  ngOnInit() {
    this.index = -1;
    this.formGroup = this.formBuilder.group({
      url: ['', [Validators.required, Validators.pattern(this.regex)]],
      zusatz: ['', [Validators.required, Validators.maxLength(255)]]
    });

    if (this.data) {
      this.formGroup.setValue(this.data.source);
      this.index = this.data.index;
    }
  }

  onTestUrlClicked(evt: any) {
    if (this.formGroup.controls.url.valid) {
      window.open(this.formGroup.controls.url.value, '_blank');
    }
  }

  onSaveClicked(evt: any) {
    this.formGroup.markAsDirty();

    if (this.formGroup.valid) {
      const result: SourceDialogData = {
        index: this.index,
        source: this.formGroup.value as QuelleAdd
      };

      this.dialogRef.close(result);
    }
  }

  onCancelClicked() {
    this.dialogRef.close(null);
  }
}
