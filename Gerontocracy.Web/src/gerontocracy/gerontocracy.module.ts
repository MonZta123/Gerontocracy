import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GerontocracyRoutingModule } from './gerontocracy-routing.module';
import { GerontocracyComponent } from './components/gerontocracy.component';
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { MenubarModule } from 'primeng/menubar';
import { SidebarModule } from 'primeng/sidebar';
import { ToolbarModule } from 'primeng/toolbar';
import { MenuModule } from 'primeng/menu';
import { ButtonModule } from 'primeng/button';
import { CardModule } from 'primeng/card';
import { InputTextModule } from 'primeng/inputtext';
import { DialogModule } from 'primeng/dialog';
import { MessagesModule } from 'primeng/messages';
import { MessageModule } from 'primeng/message';
import { BlockUIModule } from 'primeng/blockui';
import { CheckboxModule } from 'primeng/checkbox';
import { ProgressSpinnerModule } from 'primeng/progressspinner';
import { SplitButtonModule } from 'primeng/splitbutton';
import { PanelModule } from 'primeng/panel';
import { DynamicDialogModule } from 'primeng/dynamicdialog';

import { ConfirmemailComponent } from './components/confirmemail/confirmemail.component';
import { LoginDialogComponent } from './components/login-dialog/login-dialog.component';
import { RegisterDialogComponent } from './components/register-dialog/register-dialog.component';
import { RegistrationConfirmDialogComponent } from './components/registration-confirm-dialog/registration-confirm-dialog.component';

@NgModule({
  declarations: [
    GerontocracyComponent,
    ConfirmemailComponent,
    LoginDialogComponent,
    RegisterDialogComponent,
    RegistrationConfirmDialogComponent,
  ],
  imports: [
    GerontocracyRoutingModule,

    BrowserModule,
    BrowserAnimationsModule,

    ReactiveFormsModule,
    HttpClientModule,

    SidebarModule,
    ToolbarModule,
    ButtonModule,
    ProgressSpinnerModule,
    MenubarModule,
    SidebarModule,
    MenuModule,
    CardModule,
    DialogModule,
    InputTextModule,
    MessagesModule,
    MessageModule,
    BlockUIModule,
    CheckboxModule,
    SplitButtonModule,
    PanelModule,
    DynamicDialogModule
  ],
  providers: [
  ],
  bootstrap: [GerontocracyComponent],
  entryComponents: [
    RegisterDialogComponent,
    LoginDialogComponent,
    RegistrationConfirmDialogComponent
  ]
})
export class AppModule { }
