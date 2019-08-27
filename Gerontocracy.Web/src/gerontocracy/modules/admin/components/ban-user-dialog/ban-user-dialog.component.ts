import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { DynamicDialogRef, SelectItem } from 'primeng/api';

@Component({
  selector: 'app-ban-user-dialog',
  templateUrl: './ban-user-dialog.component.html',
  styleUrls: ['./ban-user-dialog.component.scss']
})
export class BanUserDialogComponent implements OnInit {

  times: SelectItem[] = [
    { label: '24 Stunden', value: '1.00:00:00' },
    { label: '48 Stunden', value: '2.00:00:00' },
    { label: '48 Stunden', value: '3.00:00:00' },
    { label: '1 Woche', value: '7.00:00:00' },
    { label: '2 Wochen', value: '14.00:00:00' },
    { label: '30 Tage', value: '30.00:00:00' },
    { label: '180 Tage', value: '180.00:00:00' },
    { label: '365 Tage', value: '365.00:00:00' },
    { label: 'Permanent', value: null }
  ];

  formGroup: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private dialogReference: DynamicDialogRef) { }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      duration: '1.00:00:00',
      reason: null
    });
  }

  confirmBan() {
    this.close(this.formGroup.value);
  }
  cancel() {
    this.close(null);
  }

  close(value: any) {
    this.dialogReference.close(value);
  }
}
