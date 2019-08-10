import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MatDialog } from '@angular/material';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { AccountService } from '../services/account.service';
import { User } from '../models/user';

@Component({
    selector: 'app-gerontocracy',
    templateUrl: './gerontocracy.component.html',
    styleUrls: ['./gerontocracy.component.scss', '../geronto-global-styles.scss']
})
export class GerontocracyComponent implements OnInit {

    accountData: User;
    isLoading: boolean;
    constructor(
        private dialog: MatDialog,
        private accountService: AccountService
    ) { }

    public ngOnInit(): void {
        this.isLoading = true;
        this.accountService
            .getCurrentUser()
            .toPromise()
            .then(n => this.accountData = n)
            .then(_ => this.isLoading = false)
            .catch(() => this.accountData = null)
            .then(_ => this.isLoading = false);
    }

    public login(event: any) {
        this.dialog.open(LoginComponent, { width: '512px', disableClose: true, hasBackdrop: true })
            .afterClosed()
            .toPromise()
            .then(n => {
                if (n) {
                    location.reload();
                }
            });
    }

    public signup(event: any) {
        this.dialog.open(RegisterComponent, { width: '512px', disableClose: true, hasBackdrop: true })
            .afterClosed()
            .toPromise()
            .then(n => {
                if (n) {
                    location.reload();
                }
            });
    }

    public logout(event: any) {
        this.accountService
            .logoutUser()
            .toPromise()
            .then(() => location.reload());
    }

    public get isAdmin() {
        return this.accountData && this.accountData.roles.find(n => n === 'admin');
    }
}
