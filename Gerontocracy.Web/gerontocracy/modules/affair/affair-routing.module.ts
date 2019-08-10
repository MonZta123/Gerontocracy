import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { AffairComponent } from './components/affair.component';
import { OverviewComponent } from './components/overview/overview.component';
import { AddComponent } from './components/add/add.component';

export const routes: Routes = [
    {
        path: '',
        component: AffairComponent,
        children: [
            {
                path: 'top/:id',
                component: OverviewComponent
            },
            {
                path: 'top',
                component: OverviewComponent
            },
            {
                path: 'new/:id',
                component: OverviewComponent
            },
            {
                path: 'new',
                component: OverviewComponent
            },
            {
                path: 'add',
                component: AddComponent
            },
            {
                path: '',
                component: OverviewComponent
            },
            {
                path: '**',
                component: OverviewComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AffairRoutingModule { }
