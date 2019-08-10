import { Component, OnInit, Inject } from '@angular/core';
import { SharedPartyService } from '../../../../modules/shared/services/shared-party.service';
import { FormBuilder, FormGroup, ValidatorFn, ValidationErrors, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA, MatAutocompleteSelectedEvent } from '@angular/material';
import { PolitikerSelection } from '../../../shared/models/politiker-selection';

@Component({
  selector: 'app-politician-selection-dialog',
  templateUrl: './politician-selection-dialog.component.html',
  styleUrls: ['./politician-selection-dialog.component.scss']
})
export class PoliticianSelectionDialogComponent implements OnInit {

  constructor(
    private formBuilder: FormBuilder,
    private sharedPartyService: SharedPartyService,
    public dialogRef: MatDialogRef<PoliticianSelectionDialogComponent>
  ) {
    this.formGroup = this.formBuilder.group({
      politikerName: [{ value: null, disabled: false }],
      beschreibung: ['', [Validators.maxLength(4000)]],
      reputationType: [null],
      politikerId: [null],
    });

    this.formGroup.setValidators(this.lockedValidator());
  }

  isRefreshingAutocomplete: boolean;
  isLoading: boolean;
  formGroup: FormGroup;

  options: PolitikerSelection[];

  ngOnInit() {

  }

  public lockedValidator(): ValidatorFn {
    return (n: FormGroup): ValidationErrors => {
      if (n.controls.politikerName.value &&
        n.controls.politikerName.value.length > 0 &&
        !(n.controls.politikerName.disabled && n.controls.politikerId)) {
        n.controls.politikerName.setErrors({ notLocked: true });
      }

      return;
    };
  }

  onKeyUp(evt: any) {
    if (!this.isRefreshingAutocomplete) {
      this.isRefreshingAutocomplete = true;
      this.sharedPartyService
        .getPoliticianSelection(evt.srcElement.value)
        .toPromise()
        .then(n => this.options = n)
        .then(() => this.isRefreshingAutocomplete = false);
    }
  }

  lockPolitiker(evt: MatAutocompleteSelectedEvent) {
    const option = evt.option.value as PolitikerSelection;

    this.formGroup.controls.politikerName.disable();
    this.formGroup.controls.politikerName.setValue(this.selectionToText(option));
    this.formGroup.controls.politikerId.setValue(option.id);
  }

  unlockPolitiker() {
    this.formGroup.controls.politikerName.setValue(null);
    this.formGroup.controls.politikerName.enable();
    this.formGroup.controls.politikerId.reset();
  }

  reset() {
    this.unlockPolitiker();
    this.formGroup.reset();
  }

  okClicked(evt: any) {
    if (this.formGroup.valid) {
      let returnValue = 0;

      if (this.formGroup.controls.politikerId.value) {
        returnValue = this.formGroup.controls.politikerId.value;
      }

      this.dialogRef.close({
        reputationType: this.formGroup.controls.reputationType.value,
        politikerId: this.formGroup.controls.politikerId.value,
        beschreibung: this.formGroup.controls.beschreibung.value
      });
    }
  }

  private selectionToText(selection: PolitikerSelection): string {
    let text = '';

    if (selection.akadGradPre) {
      text = `${selection.akadGradPre} `;
    }

    text = `${text}${selection.vorname} ${selection.nachname}`;

    if (selection.akadGradPost) {
      text = `${text}, ${selection.akadGradPost}`;
    }

    return text;
  }
}
