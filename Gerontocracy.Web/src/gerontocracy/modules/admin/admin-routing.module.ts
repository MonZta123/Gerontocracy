import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './components/admin.component';
import { UserviewComponent } from './components/userview/userview.component';

export const routes: Routes = [
    {
        path: '',
        component: AdminComponent,
        children: [
            {
                path: 'user/:id',
                component: UserviewComponent
            },
            {
                path: 'user',
                component: UserviewComponent,
            },
            {
                path: '',
                component: UserviewComponent
            },
            {
                path: '**',
                component: UserviewComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
