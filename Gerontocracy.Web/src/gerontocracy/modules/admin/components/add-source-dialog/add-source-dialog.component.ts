import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/api';
import { RssDialogResult } from './source-dialog-data';

@Component({
  selector: 'app-add-source-dialog',
  templateUrl: './add-source-dialog.component.html',
  styleUrls: ['./add-source-dialog.component.scss']
})
export class AddSourceDialogComponent implements OnInit {

  formGroup: FormGroup;

  private index: number;
  private readonly regex = '((https?://)|(http?://))([\\da-z.-]+)\\.([a-z.]{2,6})[/\\w .-]*/?';

  constructor(
    private dialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
    private formBuilder: FormBuilder,
  ) {
    dialogConfig.header = `Quelle ${dialogConfig.data ? 'bearbeiten' : 'hinzufügen'}`;
  }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      url: ['', [Validators.required, Validators.pattern(this.regex)]],
      name: ['', [Validators.required, Validators.maxLength(255)]]
    });
  }

  onTestUrlClicked() {
    if (this.formGroup.controls.url.valid) {
      window.open(this.formGroup.controls.url.value, '_blank');
    }
  }

  onSaveClicked() {
    this.formGroup.markAsDirty();

    if (this.formGroup.valid) {
      const result: RssDialogResult = {
        id: this.dialogConfig.data.id,
        ...this.formGroup.value
      };

      this.dialogRef.close(result);
    }
  }

  onCancelClicked() {
    this.dialogRef.close();
  }
}
