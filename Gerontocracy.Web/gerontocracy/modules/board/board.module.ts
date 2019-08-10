import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { BoardComponent } from './components/board.component';
import { OverviewComponent } from './components/overview/overview.component';
import { BoardRoutingModule } from './board-routing.module';
import { ThreadComponent } from './components/thread/thread.component';
import { AddComponent } from './components/add/add.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';

import { PostComponent } from './components/post/post.component';
import { SharedModule } from '../shared/shared.module';

import {
  MatTabsModule,
  MatTableModule,
  MatButtonModule,
  MatDialogModule,
  MatProgressSpinnerModule,
  MatSidenavModule,
  MatIconModule,
  MatCardModule,
  MatExpansionModule,
  MatFormFieldModule,
  MatPaginatorModule,
  MatInputModule,
  MatTreeModule,
  MatAutocompleteModule
} from '@angular/material';

@NgModule({
  declarations: [
    BoardComponent,
    OverviewComponent,
    ThreadComponent,
    AddComponent,
    PostComponent
  ],
  imports: [
    CommonModule,

    FormsModule,
    ReactiveFormsModule,

    HttpClientModule,

    BoardRoutingModule,

    SharedModule,

    MatTabsModule,
    MatTableModule,
    MatPaginatorModule,
    MatDialogModule,
    MatButtonModule,
    MatProgressSpinnerModule,
    MatSidenavModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatAutocompleteModule,
    MatIconModule,
    MatInputModule,
    MatCardModule,
    MatTreeModule,
  ]
})
export class BoardModule { }
