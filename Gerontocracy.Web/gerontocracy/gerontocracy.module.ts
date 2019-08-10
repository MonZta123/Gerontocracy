import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { GerontocracyRoutingModule } from './gerontocracy-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { ReactiveFormsModule } from '@angular/forms';

import { GerontocracyComponent } from './components/gerontocracy.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

import { HttpClientModule } from '@angular/common/http';

import { AccountService } from './services/account.service';
import { SharedModule } from './modules/shared/shared.module';
import { MessageBoxComponent } from './modules/shared/components/message-box/message-box.component';
import { ConfirmemailComponent } from './components/confirmemail/confirmemail.component';


import {
  MatSidenavModule,
  MatIconModule,
  MatButtonModule,
  MatToolbarModule,
  MatListModule,
  MatCheckboxModule,
  MatFormFieldModule,
  MatMenuModule,
  MatInputModule,
  MatDialogModule,
  MatProgressSpinnerModule,
  MAT_DIALOG_DEFAULT_OPTIONS,
  MAT_LABEL_GLOBAL_OPTIONS
} from '@angular/material';

@NgModule({
  declarations: [
    GerontocracyComponent,
    LoginComponent,
    RegisterComponent,
    ConfirmemailComponent
  ],
  imports: [
    BrowserModule,
    GerontocracyRoutingModule,

    ReactiveFormsModule,

    BrowserAnimationsModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatSidenavModule,
    MatButtonModule,
    MatCheckboxModule,
    MatFormFieldModule,
    MatDialogModule,
    MatMenuModule,
    MatInputModule,
    MatProgressSpinnerModule,

    SharedModule,

    HttpClientModule,
  ],
  entryComponents: [
    LoginComponent,
    MessageBoxComponent,
    RegisterComponent,
  ],
  providers: [
    AccountService,
    { provide: MAT_LABEL_GLOBAL_OPTIONS, useValue: { float: 'always' } },
    { provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: { hasBackdrop: false } }
  ],
  bootstrap: [
    GerontocracyComponent
  ]
})
export class GerontocracyModule { }
