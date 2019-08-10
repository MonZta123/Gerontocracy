import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PartyComponent } from './components/party.component';
import { OverviewComponent } from './components/overview/overview.component';
import { PartyRoutingModule } from './party-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { PartyService } from './services/party.service';
import { DetailComponent } from './components/detail/detail.component';
import { SharedModule } from '../shared/shared.module';

import {
  MatProgressSpinnerModule,
  MatDividerModule,
  MatFormFieldModule,
  MatIconModule,
  MatInputModule,
  MatCheckboxModule,
  MatTableModule,
  MatButtonModule,
  MatPaginatorModule,
  MatSidenavModule,
  MatCardModule,
  MatExpansionModule,
  MatDialogModule
} from '@angular/material';

@NgModule({
  declarations: [PartyComponent, OverviewComponent, DetailComponent],
  imports: [
    CommonModule,

    FormsModule,
    ReactiveFormsModule,

    HttpClientModule,

    PartyRoutingModule,

    SharedModule,

    MatProgressSpinnerModule,
    MatDividerModule,
    MatFormFieldModule,
    MatIconModule,
    MatInputModule,
    MatCheckboxModule,
    MatTableModule,
    MatButtonModule,
    MatPaginatorModule,
    MatSidenavModule,
    MatCardModule,
    MatExpansionModule,
    MatDialogModule
  ],
  providers: [PartyService]
})
export class PartyModule { }
