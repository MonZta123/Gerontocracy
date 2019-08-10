import { Component, OnInit } from '@angular/core';
import { SharedAccountService } from '../../shared/services/shared-account.service';
import { MatDialog } from '@angular/material';
import { Router } from '@angular/router';
import { LoginComponent } from '../../../components/login/login.component';

@Component({
  selector: 'app-board',
  templateUrl: './board.component.html',
  styleUrls: ['./board.component.scss', '../../../geronto-global-styles.scss']
})
export class BoardComponent implements OnInit {

  isLoggedIn: boolean;
  isLoadingData: boolean;

  constructor(
    private router: Router,
    private sharedAccountService: SharedAccountService,
    private dialog: MatDialog,

  ) {
    const links = [
      {
        label: 'Top Threads',
        path: './top',
        index: 0
      },
      {
        label: 'Neueste Threads',
        path: './new',
        index: 1
      }
    ];
    this.isLoadingData = true;
    this.sharedAccountService.isLoggedIn().toPromise().then(n => {
      this.isLoggedIn = n;
      if (n) {
        links.push({
          label: 'Neuer Eintrag',
          path: './add',
          index: 2
        });
      }

      this.navLinks = links;
    })
    .then(_ => this.isLoadingData = false);
  }

  navLinks: any[];
  activeLinkIndex = -1;

  showRegister(): void {
    this.dialog.open(LoginComponent, { width: '512px', disableClose: true, hasBackdrop: true })
      .afterClosed()
      .toPromise()
      .then(m => {
        if (m) {
          location.reload();
        }
      });
  }

  ngOnInit(): void {
    this.router.events.subscribe((res) => {
      this.activeLinkIndex = this.navLinks.indexOf(this.navLinks.find(tab => tab.link === '.' + this.router.url));
    });
  }
}
