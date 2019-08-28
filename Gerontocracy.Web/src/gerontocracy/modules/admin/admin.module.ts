import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './components/admin.component';
import { UserviewComponent } from './components/userview/userview.component';
import { UserdetailComponent } from './components/userdetail/userdetail.component';
import { AdminRoutingModule } from './admin-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';
import { InputTextModule } from 'primeng/inputtext';
import { TableModule } from 'primeng/table';
import { PaginatorModule } from 'primeng/paginator';
import { DialogModule } from 'primeng/dialog';
import { ButtonModule } from 'primeng/button';
import { PanelModule } from 'primeng/panel';
import { MessageModule } from 'primeng/message';
import { MessagesModule } from 'primeng/messages';
import { BlockUIModule } from 'primeng/blockui';
import { ToastModule } from 'primeng/toast';
import { DropdownModule } from 'primeng/dropdown';
import { MessageService } from 'primeng/api';
import { AddRoleDialogComponent } from './components/add-role-dialog/add-role-dialog.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { TaskviewComponent } from './components/taskview/taskview.component';
import { TaskdetailComponent } from './components/taskdetail/taskdetail.component';
import { CheckboxModule } from 'primeng/checkbox';
import { TaskTypePipe } from './pipes/task-type.pipe';
import { BanUserDialogComponent } from './components/ban-user-dialog/ban-user-dialog.component';
import { UnbanUserDialogComponent } from './components/unban-user-dialog/unban-user-dialog.component';


@NgModule({
  declarations: [
    AdminComponent,
    UserviewComponent,
    UserdetailComponent,
    AddRoleDialogComponent,
    TaskviewComponent,
    TaskdetailComponent,
    TaskTypePipe,
    BanUserDialogComponent,
    UnbanUserDialogComponent
  ],
  imports: [
    CommonModule,

    AdminRoutingModule,

    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,

    TableModule,
    PaginatorModule,
    DialogModule,
    ButtonModule,
    PanelModule,
    InputTextModule,
    MessagesModule,
    MessageModule,
    BlockUIModule,
    ToastModule,
    AutoCompleteModule,
    DropdownModule,
    CheckboxModule
  ],
  providers: [
    MessageService
  ],
  entryComponents: [
    BanUserDialogComponent,
    UnbanUserDialogComponent,
    AddRoleDialogComponent
  ]
})
export class AdminModule { }
