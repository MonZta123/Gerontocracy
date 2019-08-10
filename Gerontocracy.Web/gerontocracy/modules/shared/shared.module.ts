import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MessageBoxComponent } from './components/message-box/message-box.component';

import { MessageService } from './services/message.service';
import { SharedPartyService } from './services/shared-party.service';
import { SharedAccountService } from './services/shared-account.service';
import { NavTopBarComponent } from './components/nav-top-bar/nav-top-bar.component';

import {
  MatDialogModule,
  MatButtonModule,
  MatIconModule,
  MAT_DIALOG_DEFAULT_OPTIONS,
  MatSnackBarModule
} from '@angular/material';
import { StringTrimPipe } from './pipes/string-trim.pipe';
import { YesNoBoxComponent } from './components/yes-no-box/yes-no-box.component';

@NgModule({
  declarations: [
    MessageBoxComponent,
    NavTopBarComponent,
    StringTrimPipe,
    YesNoBoxComponent,
  ],
  exports: [
    NavTopBarComponent,

    StringTrimPipe
  ],
  imports: [
    CommonModule,

    MatIconModule,
    MatDialogModule,
    MatButtonModule,
    MatSnackBarModule
  ],
  entryComponents: [
    MessageBoxComponent,
    YesNoBoxComponent
  ],
  providers: [
    MessageService,
    SharedAccountService,
    SharedPartyService,
    { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { hasBackdrop: false } }
  ],
})
export class SharedModule { }
