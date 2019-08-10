import { NgModule } from '@angular/core';
import { OverviewComponent } from './components/overview/overview.component';
import { HomeComponent } from './components/home.component';
import { HomeRoutingModule } from './home-routing.module';
import { CommonModule } from '@angular/common';

import {
  MatGridListModule,
  MatCardModule,
  MatListModule,
  MatProgressSpinnerModule,
  MatButtonModule,
  MatIconModule,
  MatTooltipModule,
  MatFormFieldModule,
  MatDialogModule,
  MatAutocompleteModule,
  MatInputModule,
  MatButtonToggleModule
} from '@angular/material';
import { SharedModule } from '../shared/shared.module';
import { PoliticianSelectionDialogComponent } from './components/politician-selection-dialog/politician-selection-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    HomeComponent,
    OverviewComponent,
    PoliticianSelectionDialogComponent
  ],
  imports: [
    CommonModule,
    HomeRoutingModule,
    MatGridListModule,
    MatCardModule,
    MatListModule,
    SharedModule,
    MatProgressSpinnerModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule,
    MatFormFieldModule,
    MatDialogModule,
    MatAutocompleteModule,
    MatInputModule,
    MatButtonToggleModule,

    ReactiveFormsModule
  ],
  entryComponents: [
    PoliticianSelectionDialogComponent
  ]
})
export class HomeModule { }
