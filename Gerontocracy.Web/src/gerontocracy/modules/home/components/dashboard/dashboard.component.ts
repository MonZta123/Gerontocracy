import { Component, OnInit } from '@angular/core';
import { DashboardData } from '../../models/dashboard-data';
import { DialogService, MessageService } from 'primeng/api';
import { SharedAccountService } from '../../../shared/services/shared-account.service';
import { Router } from '@angular/router';
import { LoginDialogComponent } from 'Gerontocracy.Web/src/gerontocracy/components/login-dialog/login-dialog.component';
import { DashboardService } from '../../services/dashboard.service';
import { PoliticianSelectionDialogComponent } from '../politician-selection-dialog/politician-selection-dialog.component';
import { NewsData } from '../../models/news-data';
import { BaseComponent } from '../../../shared/components/base/base.component';
import { SharedService } from '../../../shared/services/shared.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
  providers: [MessageService, DialogService]
})
export class DashboardComponent extends BaseComponent implements OnInit {

  constructor(
    private dashboardService: DashboardService,
    private sharedAccountService: SharedAccountService,
    private dialogService: DialogService,
    private router: Router,
    sharedService: SharedService,
    messageService: MessageService,
  ) {
    super(messageService, sharedService);
  }

  dashboard: DashboardData;

  ngOnInit() {
    this.dashboardService.getDashboardData().toPromise()
      .then(n => this.dashboard = n);
  }

  addAffair(newsId: number) {
    this.sharedAccountService.isLoggedIn().toPromise().then(r => {
      if (r) {
        this.dialogService.open(PoliticianSelectionDialogComponent,
          {
            header: 'Neuen Vorfalle einreichen',
            width: '800px',
            closable: false,
          })
          .onClose
          .subscribe(n => {
            if (n) {
              const data: NewsData = {
                beschreibung: n.beschreibung,
                newsId,
                reputationType: n.reputationType,
                politikerId: n.politikerId
              };

              this.dashboardService.generateNews(data)
                .pipe(super.start(), super.end())
                .toPromise()
                .then(o => {
                  super.handlePostResult(o);
                  this.showAffair(o.data);
                })
                .catch(error => super.handleError(error));
            }
          });
      } else {
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
    });
  }

  showAffair(affairId: number) {
    this.router.navigate([`affair/new/${affairId}`]);
  }
}
