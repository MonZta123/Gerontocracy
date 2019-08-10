import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AdminComponent } from './components/admin.component';
import { OverviewComponent } from './components/overview/overview.component';
import { AdminRoutingModule } from './admin-routing.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http';
import { SharedModule } from '../shared/shared.module';
import { PermissionComponent } from './components/permission/permission.component';
import {
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
  MatCardModule
} from '@angular/material';
import { AdminService } from './services/admin.service';
import { PermissionDetailComponent } from './components/permission-detail/permission-detail.component';

@NgModule({
  declarations: [
    AdminComponent,
    OverviewComponent,
    PermissionComponent,
    PermissionDetailComponent
  ],
  imports: [
    CommonModule,
    AdminRoutingModule,

    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    SharedModule,

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
    MatCardModule
  ],
  providers: [
    AdminService
  ]
})
export class AdminModule { }
