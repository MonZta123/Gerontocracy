import { Component, OnInit } from '@angular/core';
import { AccountService } from 'Gerontocracy.Web/src/gerontocracy/services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss']
})
export class AdminComponent implements OnInit {

  isAdmin: boolean;

  constructor(
    private accountService: AccountService,
    private router: Router) {
  }

  ngOnInit() {
    this.isAdmin = false;
    this.accountService
      .getCurrentUser()
      .toPromise()
      .then(n => {
        const roles: string[] = n.roles;
        if (roles.includes('admin') || roles.includes('moderator')) {
          this.isAdmin = true;
        } else {
          this.router.navigate(['/']);
        }
      });
  }
}
