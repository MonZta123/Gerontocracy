import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './components/admin.component';
import { UserviewComponent } from './components/userview/userview.component';
import { TaskviewComponent } from './components/taskview/taskview.component';

export const routes: Routes = [
    {
        path: '',
        component: AdminComponent,
        children: [
            {
                path: 'task/:id',
                component: TaskviewComponent
            },
            {
                path: 'task',
                component: TaskviewComponent
            },
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
                component: TaskviewComponent
            },
            {
                path: '**',
                component: TaskviewComponent
            }
        ]
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class AdminRoutingModule { }
