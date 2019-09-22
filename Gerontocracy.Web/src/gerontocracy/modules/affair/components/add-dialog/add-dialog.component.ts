import { Component, OnInit, } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AffairService } from '../../services/affair.service';
import { SharedPartyService } from '../../../shared/services/shared-party.service';
import { DialogService, MessageService, DynamicDialogRef } from 'primeng/api';
import { PolitikerSelection } from '../../../shared/models/politiker-selection';
import { VorfallAdd } from '../../models/vorfall-add';
import { QuelleAdd } from '../../models/quelle-add';
import { SourceDialogComponent } from '../source-dialog/source-dialog.component';
import { SourceDialogData } from '../source-dialog/source-dialog-data';
import { QuelleData } from './quelle-data';
import { ReputationType } from '../../../shared/models/reputation-type';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { SharedService } from '../../../shared/services/shared.service';

@Component({
  selector: 'app-add-dialog',
  templateUrl: './add-dialog.component.html',
  styleUrls: ['./add-dialog.component.scss'],
  providers: [DialogService]
})
export class AddDialogComponent extends BaseComponent implements OnInit {

  formGroup: FormGroup;
  sources: QuelleData[];
  showSourcesError: boolean;

  options: PolitikerSelection[];
  politiker: any;
  reputationType: ReputationType;

  public ReputationType = ReputationType;

  constructor(
    private formBuilder: FormBuilder,
    private sharedPartyService: SharedPartyService,
    private dialogService: DialogService,
    private affairService: AffairService,
    private dialogReference: DynamicDialogRef,
    messageService: MessageService,
    sharedService: SharedService) {
    super(messageService, sharedService);
  }

  ngOnInit() {
    this.sources = [];
    this.reputationType = ReputationType.Neutral;
    this.showSourcesError = false;
    this.formGroup = this.formBuilder.group({
      titel: ['', [Validators.maxLength(50), Validators.required]],
      beschreibung: ['', [Validators.maxLength(4000), Validators.required]],
      reputationType: [null],
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

  onRemoveClicked(index: number) {
    const obj = this.sources.find(n => n.index === index);
    this.sources = this.sources.filter(n => n !== obj);
  }

  get sourceData(): QuelleAdd[] {
    return this.sources;
  }

  public addSource() {
    this.dialogService.open(SourceDialogComponent, {
      closable: false,
      width: '512px',
    }).onClose
      .subscribe((n: SourceDialogData) => {
        if (n) {
          this.sources.push({ index: n.index, ...n.source });
          this.showSourcesError = false;
        }
      });
  }

  public addAffair() {
    if (this.formGroup.valid && this.sources.length > 0) {
      const obj: VorfallAdd = this.formGroup.value;
      obj.quellen = this.sources;
      if (this.politiker) {
        obj.politikerId = this.politiker.id;
        obj.reputationType = this.reputationType;
      }

      this.affairService.addAffair(obj)
        .pipe(super.start(), super.end())
        .toPromise()
        .then(n => {
          super.handlePostResult(n);
          if (n.success) {
            console.log(n.data);
            window.location.href = `/affair/new/${n.data}`;
          }
        })
        .catch(error => super.handleError(error));

    } else {
      if (this.sources.length < 1) {
        this.showSourcesError = true;
      }
    }
  }

  close() {
    this.dialogReference.close();
  }
}
