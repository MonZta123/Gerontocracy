import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ValidationErrors, ValidatorFn } from '@angular/forms';
import { VorfallAdd } from '../../models/vorfall-add';
import { AffairService } from '../../services/affair.service';
import { MatDialog, MatAutocompleteSelectedEvent, MatAutocompleteTrigger } from '@angular/material';
import { SourceDialogComponent } from '../source-dialog/source-dialog.component';
import { SourceDialogData } from '../source-dialog/source-dialog-data';
import { SourcesDataSource } from './sources-data-source';
import { QuelleAdd } from '../../models/quelle-add';
import { PolitikerSelection } from '../../../shared/models/politiker-selection';
import { SharedPartyService } from '../../../shared/services/shared-party.service';
import { MessageService } from '../../../shared/services/message.service';
import { Router } from '@angular/router';
import { SharedAccountService } from '../../../../modules/shared/services/shared-account.service';
import { ReputationType } from '../../../shared/models/reputation-type';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss', '../../../../geronto-global-styles.scss']
})
export class AddComponent implements OnInit {
  @ViewChild('politikerInput', { read: MatAutocompleteTrigger }) politikerAutoComplete: MatAutocompleteTrigger;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private affairService: AffairService,
    private sharedPartyService: SharedPartyService,
    private sharedAccountService: SharedAccountService,
    private messageService: MessageService,
    private matDialog: MatDialog) {

    this.sources = new SourcesDataSource();
    this.isLoading = false;
    this.isRefreshingAutocomplete = false;

    this.showSourcesError = false;
    this.isFormChecked = false;

    this.formGroup = this.formBuilder.group({
      titel: ['', [Validators.maxLength(50), Validators.required]],
      beschreibung: ['', [Validators.maxLength(4000), Validators.required]],
      reputationType: [null],
      politikerName: [{ value: null, disabled: false }],
      politikerId: [null],
    });

    this.formGroup.setValidators(this.lockedValidator());
  }

  isRefreshingAutocomplete: boolean;
  isLoading: boolean;
  formGroup: FormGroup;
  sources: SourcesDataSource;
  showSourcesError: boolean;
  isFormChecked: boolean;

  options: PolitikerSelection[];

  displayedColumns = ['position', 'text', 'url', 'call', 'delete'];

  ngOnInit() {
    this.isLoading = true;
    this.sharedAccountService.isLoggedIn().toPromise().then(n => {
      if (!n) {
        this.router.navigate(['/affair']);
      } else {
        this.isLoading = false;
      }
    });
  }

  public lockedValidator(): ValidatorFn {
    return (n: FormGroup): ValidationErrors => {
      if (n.controls.politikername && n.controls.politikerName.value.length > 0 && !(n.controls.politikerName.disabled && n.controls.politikerId)) {
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

    this.formGroup.controls.reputationType.setValue(ReputationType.Neutral);
    this.formGroup.controls.politikerName.disable();
    this.formGroup.controls.politikerName.setValue(this.selectionToText(option));
    this.formGroup.controls.politikerId.setValue(option.id);
  }

  unlockPolitiker() {
    this.formGroup.controls.reputationType.setValue(null);
    this.formGroup.controls.politikerName.setValue(null);
    this.formGroup.controls.politikerName.enable();
    this.formGroup.controls.politikerId.reset();
  }

  onTestUrlClicked(url: string) {
    window.open(url, '_blank');
  }

  onRemoveClicked(index: number) {
    this.sources.remove(index);
  }

  get sourceData(): QuelleAdd[] {
    return this.sources.getData();
  }

  public addSource() {
    this.matDialog.open(SourceDialogComponent, {
      disableClose: true,
      width: '512px',
      data: null
    }).afterClosed().toPromise().then((n: SourceDialogData) => {
      if (n) {
        this.sources.add(n.source);
        this.showSourcesError = false;
      }
    });
  }

  public addAffair() {
    this.isFormChecked = true;
    if (this.formGroup.valid && this.sources.data.value.length > 0) {
      this.isLoading = true;

      const obj: VorfallAdd = this.formGroup.value;
      obj.quellen = this.sources.getData();

      this.affairService.addAffair(obj)
        .toPromise()
        .then(() => this.isLoading = false)
        .then(() => this.router.navigate(['/affair']))
        .catch(n => {
          this.messageService.showSnackbar('Ein Fehler ist aufgetreten!', 'Keine Ahnung!');
        })
        .then(() => this.isLoading = false);

    } else {
      if (this.sources.data.value.length < 1) {
        this.showSourcesError = true;
      }
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

  public shorten(url: string) {
    return url;
  }
}
