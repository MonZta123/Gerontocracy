import { Component, OnInit } from '@angular/core';
import { PolitikerSelection } from '../../../shared/models/politiker-selection';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { DynamicDialogRef } from 'primeng/api';
import { SharedPartyService } from '../../../shared/services/shared-party.service';
import { ReputationType } from '../../../shared/models/reputation-type';

@Component({
  selector: 'app-politician-selection-dialog',
  templateUrl: './politician-selection-dialog.component.html',
  styleUrls: ['./politician-selection-dialog.component.scss']
})
export class PoliticianSelectionDialogComponent implements OnInit {

  formGroup: FormGroup;
  options: PolitikerSelection[];
  politiker: any;

  reputationType: ReputationType;

  public ReputationType = ReputationType;

  constructor(
    private formBuilder: FormBuilder,
    private sharedPartyService: SharedPartyService,
    private dialogReference: DynamicDialogRef) { }

  ngOnInit() {
    this.reputationType = ReputationType.Neutral;
    this.formGroup = this.formBuilder.group({
      beschreibung: ['', [Validators.required, Validators.maxLength(4000)]],
    });
  }

  getSuggestions(evt: any) {
    this.sharedPartyService
      .getPoliticianSelection(evt.query)
      .toPromise()
      .then(n => this.options = n);
  }

  unlockPolitiker() {
    this.politiker = null;
  }

  ok() {
    if (this.formGroup.valid) {
      let politikerId: number = null;
      if (this.politiker) {
        politikerId = this.politiker.id;
      }

      this.dialogReference.close({
        reputationType: this.reputationType,
        politikerId,
        beschreibung: this.formGroup.controls.beschreibung.value
      });
    }
  }

  close(refreshScreen: boolean) {
    this.dialogReference.close(refreshScreen);
  }
}
