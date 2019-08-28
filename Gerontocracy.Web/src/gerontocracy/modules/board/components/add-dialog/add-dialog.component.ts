import { Component, OnInit } from '@angular/core';
import { DynamicDialogConfig, DynamicDialogRef, MessageService } from 'primeng/api';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SharedAccountService } from '../../../shared/services/shared-account.service';
import { SharedBoardService } from '../../../shared/services/shared-board.service';
import { BoardService } from '../../services/board.service';
import { Router } from '@angular/router';
import { VorfallSelection } from '../../../shared/models/vorfall-selection';
import { ThreadData } from '../../models/thread-data';

@Component({
  selector: 'app-add-dialog',
  templateUrl: './add-dialog.component.html',
  styleUrls: ['./add-dialog.component.scss'],
})
export class AddDialogComponent implements OnInit {

  formGroup: FormGroup;

  options: VorfallSelection[];

  selectedSelection: any;

  constructor(
    private formBuilder: FormBuilder,
    private boardService: BoardService,
    private sharedBoardService: SharedBoardService,
    private dialogReference: DynamicDialogRef) { }

  getSuggestions(evt: any) {
    this.sharedBoardService
      .getAffairSelection(evt.query)
      .toPromise()
      .then(n => this.options = n.map(m => ({ ...m, fulltext: `${m.titel} (${m.user})` })));
  }

  unlockSelection() {
    this.selectedSelection = null;
  }

  ngOnInit() {
    this.formGroup = this.formBuilder.group({
      titel: ['', [Validators.maxLength(200), Validators.required]],
      content: ['', [Validators.maxLength(4000), Validators.required]]
    });
  }

  public addThread() {
    if (this.formGroup.valid) {
      const data: any = this.formGroup.value as ThreadData;
      if (this.selectedSelection) {
        data.vorfallId = this.selectedSelection.id;
      }

      this.boardService.addThread(this.formGroup.value as ThreadData)
        .toPromise()
        .then(() => {
          this.close(true);
        });
    }
  }

  close(refreshScreen: boolean) {
    this.dialogReference.close(refreshScreen);
  }
}
