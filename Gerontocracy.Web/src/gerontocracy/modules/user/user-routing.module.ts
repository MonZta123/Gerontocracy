
import { NgModule } from '@angular/core';

import { RouterModule, Routes } from '@angular/router';
import { UserComponent } from './components/user.component';

export const routes: Routes = [
    {
        path: ':name',
        component: UserComponent,
    },
    {
        path: '',
        component: UserComponent,
    }
];

@NgModule({
    imports: [RouterModule.forChild(routes)],
    exports: [RouterModule]
})
export class UserRoutingModule { }
