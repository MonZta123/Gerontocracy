import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { MenuItem } from 'primeng/components/common/menuitem';
import { User } from '../models/user';
import { AccountService } from '../services/account.service';
import { DialogService } from 'primeng/api';
import { LoginDialogComponent } from './login-dialog/login-dialog.component';
import { RegisterDialogComponent } from './register-dialog/register-dialog.component';
import { SharedService } from '../modules/shared/services/shared.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './gerontocracy.component.html',
  styleUrls: ['./gerontocracy.component.scss'],
  providers: [DialogService]
})
export class GerontocracyComponent implements OnInit {
  @ViewChild('registrationTitle', { static: false }) registrationTitle: ElementRef;
  @ViewChild('registrationContent', { static: false }) registrationContent: ElementRef;
  @ViewChild('errorTitle', { static: false }) errorTitle: ElementRef;
  @ViewChild('errorMessage', { static: false }) errorMessage: ElementRef;
  @ViewChild('loginErrorContent', { static: false }) loginErrorContent: ElementRef;

  items: MenuItem[] = [];
  userMenu: MenuItem[] = [];
  burger: MenuItem[] = [];

  accountData: User;
  sidebarVisible: boolean;
  isLoadingMain: boolean;
  registerConfirmVisible: boolean;

  constructor(
    private sharedService: SharedService,
    private router: Router,
    private accountService: AccountService,
    private dialogService: DialogService) {
  }

  ngOnInit() {
    this.isLoadingMain = true;
    this.burger = [{
      icon: 'pi pi-bars',
      command: () => this.toggleSidenav()
    }];

    this.userMenu = [{
      label: 'Logout',
      command: () => this.logout(),
      icon: 'pi pi-sign-out'
    }];

    this.accountService
      .getCurrentUser()
      .toPromise()
      .then(n => this.accountData = n)
      .then(() => {
        const urls: MenuItem[] = [
          {
            label: 'Gerontocracy',
            icon: 'pi pi-briefcase',
            url: '/'
          },
          {
            label: 'Home',
            icon: 'pi pi-home',
            routerLink: '/'
          },
          {
            label: 'Parteiübersicht',
            icon: 'pi pi-users',
            routerLink: '/party'
          },
          {
            label: 'Vorfälle',
            icon: 'pi pi-folder-open',
            routerLink: '/affair'
          },
          {
            label: 'Boards',
            icon: 'pi pi-comments',
            routerLink: '/board'
          }
        ];

        if (this.accountData && this.accountData.roles.find(n => n === 'admin' || n === 'moderator')) {
          const subItems = [{
            label: 'Aufgaben',
            icon: 'pi pi-list',
            routerLink: '/admin/task',
          },
          {
            label: 'Users',
            icon: 'pi pi-users',
            routerLink: '/admin/user'
          }];

          if (this.accountData.roles.find(m => m === 'admin')) {
            subItems.push(
              {
                label: 'Einstellungen',
                icon: 'pi pi-globe',
                routerLink: '/admin/settings'
              });
          }

          urls.push({
            label: 'Admin',
            icon: 'pi pi-cog',
            routerLink: '/admin/task',
            items: subItems
          });
        }

        this.items = urls;
      })
      .then(() => this.isLoadingMain = false)
      .catch(() => this.accountData = null)
      .then(() => this.isLoadingMain = false);
  }

  get isLoading(): boolean {
    return this.sharedService.loading;
  }

  userClicked(): void {
    this.router.navigate(['user', this.accountData.userName]);
  }

  toggleSidenav(): void {
    this.sidebarVisible = true;
  }

  login(): void {
    this.dialogService.open(LoginDialogComponent,
      {
        header: 'Login',
        width: '407px',
        closable: false,
      })
      .onClose
      .subscribe(n => {
        if (n) {
          window.location.reload();
        }
      });
  }

  signup(): void {
    this.dialogService.open(RegisterDialogComponent,
      {
        header: 'Registrieren',
        width: '407',
        closable: false,
      }).onClose.subscribe(m => {
        if (m) {
          window.location.reload();
        }
      });
  }

  logout() {
    this.accountService
      .logoutUser()
      .toPromise()
      .then(() => window.location.reload());
  }
}
