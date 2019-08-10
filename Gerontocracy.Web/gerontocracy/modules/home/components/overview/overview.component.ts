import { Component, OnInit } from '@angular/core';
import { DashboardData } from '../../models/dashboard-data';
import { DashboardService } from '../../services/dashboard.service';
import { MessageService } from '../../../../modules/shared/services/message.service';
import { PoliticianSelectionDialogComponent } from '../politician-selection-dialog/politician-selection-dialog.component';
import { MatDialog } from '@angular/material';
import { NewsData } from '../../models/news-data';
import { Router } from '@angular/router';
import { LoginComponent } from '../../../../components/login/login.component';
import { SharedAccountService } from '../../../../modules/shared/services/shared-account.service';

@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss', '../../../../geronto-global-styles.scss']
})
export class OverviewComponent implements OnInit {

  constructor(
    private dashboardService: DashboardService,
    private messageService: MessageService,
    private sharedAccountService: SharedAccountService,
    private dialog: MatDialog,
    private router: Router
  ) { }

  dashboard: DashboardData;
  isLoading: boolean;

  ngOnInit() {
    this.isLoading = true;

    this.dashboardService.getDashboardData().toPromise()
      .then(n => this.dashboard = n)
      .then(_ => this.isLoading = false);
  }

  addAffair(newsId: number) {
    this.sharedAccountService.isLoggedIn().toPromise().then(r => {
      if (r) {
        this.dialog.open(PoliticianSelectionDialogComponent, { width: '512px', disableClose: true, hasBackdrop: true })
          .afterClosed()
          .toPromise()
          .then(n => {
            if (n !== false) {
              const data: NewsData = {
                beschreibung: n.beschreibung,
                newsId,
                reputationType: n.reputationType,
                politikerId: n.politikerId
              };

              this.dashboardService
                .generateNews(data)
                .toPromise()
                .then(o => this.showAffair(o))
                .catch(o => this.messageService.showSnackbar(o.message, 'keine Ahnung'));
            }
          });
      } else {
        this.dialog.open(LoginComponent, { width: '512px', disableClose: true, hasBackdrop: true })
          .afterClosed()
          .toPromise()
          .then(m => {
            if (m) {
              location.reload();
            }
          });
      }
    });
  }

  showAffair(affairId: number) {
    this.router.navigate([`affair/new/${affairId}`]);
  }
}
