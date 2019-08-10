import { Component, OnInit, ViewChild } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ValidationErrors, ValidatorFn } from '@angular/forms';
import { BoardService } from '../../services/board.service';
import { MatDialog, MatAutocompleteSelectedEvent, MatAutocompleteTrigger } from '@angular/material';
import { SharedBoardService } from '../../../shared/services/shared-board.service';
import { MessageService } from '../../../shared/services/message.service';
import { Router } from '@angular/router';
import { SharedAccountService } from '../../../../modules/shared/services/shared-account.service';
import { VorfallSelection } from '../../../shared/models/vorfall-selection';
import { ThreadData } from '../../models/thread-data';

@Component({
  selector: 'app-add',
  templateUrl: './add.component.html',
  styleUrls: ['./add.component.scss', '../../../../geronto-global-styles.scss']
})
export class AddComponent implements OnInit {
  @ViewChild('vorfallInput', { read: MatAutocompleteTrigger }) vorfallAutoComplete: MatAutocompleteTrigger;

  constructor(
    private router: Router,
    private formBuilder: FormBuilder,
    private boardService: BoardService,
    private sharedBoardService: SharedBoardService,
    private sharedAccountService: SharedAccountService,
    private messageService: MessageService,
    private matDialog: MatDialog) { }

  isRefreshingAutocomplete: boolean;
  isLoading: boolean;
  formGroup: FormGroup;

  options: VorfallSelection[];

  ngOnInit() {
    this.isRefreshingAutocomplete = false;
    this.formGroup = this.formBuilder.group({
      titel: ['', [Validators.maxLength(200), Validators.required]],
      content: ['', [Validators.maxLength(4000), Validators.required]],
      vorfallName: [{ value: null, disabled: false }],
      vorfallId: [null]
    });
  }

  lockVorfall(evt: MatAutocompleteSelectedEvent) {
    const option = evt.option.value as VorfallSelection;

    this.formGroup.controls.vorfallName.disable();
    this.formGroup.controls.vorfallName.setValue(this.selectionToText(option));
    this.formGroup.controls.vorfallId.setValue(option.id);
  }

  private selectionToText(selection: VorfallSelection): string {
    return `${selection.titel} ${selection.id} ${selection.user}`;
  }

  onKeyUp(evt: any) {
    if (!this.isRefreshingAutocomplete) {
      this.isRefreshingAutocomplete = true;
      this.sharedBoardService
        .getAffairSelection(evt.srcElement.value)
        .toPromise()
        .then(n => this.options = n)
        .then(() => this.isRefreshingAutocomplete = false);
    }
  }

  unlockVorfall() {
    this.formGroup.controls.vorfallName.setValue(null);
    this.formGroup.controls.vorfallName.enable();
    this.formGroup.controls.vorfallId.reset();
  }

  public addThread() {
    if (this.formGroup.valid) {
      this.isLoading = true;
      this.boardService.addThread(this.formGroup.value as ThreadData)
        .toPromise()
        .then(n => {
          this.isLoading = false;
          this.router.navigate([`/board/new/${n}`]);
        });
    }
  }
}
