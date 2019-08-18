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
import { MessageService } from 'primeng/api';


@NgModule({
  declarations: [
    AdminComponent,
    UserviewComponent,
    UserdetailComponent
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
    ToastModule
  ],
  providers: [
    MessageService
  ]
})
export class AdminModule { }
