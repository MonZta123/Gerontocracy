import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { PartyComponent } from './components/party.component';
import { OverviewComponent } from './components/overview/overview.component';

export const routes: Routes = [
    {
        path: '',
        component: PartyComponent,
        children: [
            {
                path: ':id',
                component: OverviewComponent
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
export class PartyRoutingModule { }
