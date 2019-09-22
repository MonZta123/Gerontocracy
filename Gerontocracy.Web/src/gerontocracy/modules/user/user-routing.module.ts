
import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './components/user.component';

export const routes: Routes = [
    {
        path: '',
        component: UserComponent,
        // children: [
        //     {
        //         path: 'top/:id',
        //         component: OverviewComponent
        //     },
        //     {
        //         path: 'top',
        //         component: OverviewComponent
        //     },
        //     {
        //         path: 'new/:id',
        //         component: OverviewComponent
        //     },
        //     {
        //         path: 'new',
        //         component: OverviewComponent
        //     },
        //     {
        //         path: '',
        //         component: OverviewComponent
        //     },
        //     {
        //         path: '**',
        //         component: OverviewComponent
        //     }
        // ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UserRoutingModule { }
