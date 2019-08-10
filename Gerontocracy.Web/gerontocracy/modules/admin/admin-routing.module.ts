import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { AdminComponent } from './components/admin.component';
import { PermissionComponent } from './components/permission/permission.component';
import { OverviewComponent } from './components/overview/overview.component';
import { componentFactoryName } from '@angular/compiler';

export const routes: Routes = [
    {
        path: '',
        component: AdminComponent,
        children: [
            {
                path: 'stats',
                component: OverviewComponent
            },
            {
                path: 'permission/:id',
                component: PermissionComponent
            },
            {
                path: 'permission',
                component: PermissionComponent,
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
export class AdminRoutingModule { }
