import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ConfirmemailComponent } from './components/confirmemail/confirmemail.component';

export const routes: Routes = [
  {
    path: 'confirmemail',
    component: ConfirmemailComponent,
  },
  {
    path: 'party',
    loadChildren: './modules/party/party.module#PartyModule',
    pathMatch: 'prefix'
  },
  {
    path: 'affair',
    loadChildren: './modules/affair/affair.module#AffairModule',
    pathMatch: 'prefix'
  },
  {
    path: 'board',
    loadChildren: './modules/board/board.module#BoardModule',
    pathMatch: 'prefix'
  },
  {
    path: 'home',
    loadChildren: './modules/home/home.module#HomeModule',
    pathMatch: 'prefix'
  },
  {
    path: 'admin',
    loadChildren: './modules/admin/admin.module#AdminModule',
    pathMatch: 'prefix'
  },
  {
    path: '',
    loadChildren: './modules/home/home.module#HomeModule',
    pathMatch: 'prefix'
  },
  {
    path: '**',
    redirectTo: 'home',
    pathMatch: 'prefix'
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class GerontocracyRoutingModule { }
