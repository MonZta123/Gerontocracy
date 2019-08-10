import { Component, OnInit } from '@angular/core';
import { SharedAccountService } from '../../shared/services/shared-account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.scss', '../../../geronto-global-styles.scss']
})
export class AdminComponent implements OnInit {

  constructor(
    private router: Router,
    private sharedAccountService: SharedAccountService) {

    this.sharedAccountService.whoami().toPromise().then(n => {
      if (n.roles.find(m => m === 'admin')) {
        const links = [
          {
            label: 'Statistik',
            path: './stats',
            index: 0
          },
          {
            label: 'Berechtigungen',
            path: './permission',
            index: 1
          }
        ];

        this.navLinks = links;
      } else {
        this.router.navigate(['/']);
      }
    });
  }

  navLinks: any[];
  activeLinkIndex = -1;

  ngOnInit() {
    this.router.events.subscribe((res) => {
      this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.link === '.' + this.router.url));
    });
  }

}
