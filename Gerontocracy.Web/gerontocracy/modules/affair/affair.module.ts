import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AffairComponent } from './components/affair.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { AffairRoutingModule } from './affair-routing.module';
import { OverviewComponent } from './components/overview/overview.component';
import { DetailComponent } from './components/detail/detail.component';
import { AddComponent } from './components/add/add.component';
import { SourceDialogComponent } from './components/source-dialog/source-dialog.component';
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
  MatDatepickerModule,
  MatSliderModule,
  MatNativeDateModule,
  MatTabsModule,
  MatButtonToggleModule,
  MatAutocompleteModule,
  MatDialogModule
} from '@angular/material';

@NgModule({
  declarations: [
    AffairComponent,
    OverviewComponent,
    DetailComponent,
    AddComponent,
    SourceDialogComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,

    HttpClientModule,

    AffairRoutingModule,
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
    MatDatepickerModule,
    MatSliderModule,
    MatNativeDateModule,
    MatTabsModule,
    MatButtonToggleModule,
    MatAutocompleteModule,
    MatDialogModule
  ],
  entryComponents: [
    SourceDialogComponent
  ]
})
export class AffairModule { }
