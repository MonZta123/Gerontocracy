import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PartyComponent } from './components/party.component';
import { OverviewComponent } from './components/overview/overview.component';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { PartyRoutingModule } from './party-routing.module';
import { PartyService } from './services/party.service';

import { PanelModule } from 'primeng/panel';
import { PaginatorModule } from 'primeng/paginator';
import { TableModule } from 'primeng/table';
import { ToastModule } from 'primeng/toast';
import { MessageService } from 'primeng/api';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { DetailviewComponent } from './components/detailview/detailview.component';
import { SharedModule } from '../shared/shared.module';
import { CardModule } from 'primeng/card';
import { DialogModule } from 'primeng/dialog';
import { BlockUIModule } from 'primeng/blockui';
import {CheckboxModule} from 'primeng/checkbox';

@NgModule({
  declarations: [
    PartyComponent,
    OverviewComponent,
    DetailviewComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    PartyRoutingModule,

    CardModule,
    PanelModule,
    PaginatorModule,
    TableModule,
    ToastModule,
    ButtonModule,
    InputTextModule,
    DialogModule,
    BlockUIModule,
    CheckboxModule,

    SharedModule
  ],
  providers: [
    MessageService,
    PartyService
  ]
})
export class PartyModule { }
