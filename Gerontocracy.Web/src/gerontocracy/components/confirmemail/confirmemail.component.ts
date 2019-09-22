
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { pipe } from 'rxjs';
import { BaseComponent } from '../../modules/shared/components/base/base.component';
import { MessageService } from 'primeng/api';
import { SharedService } from '../../modules/shared/services/shared.service';

@Component({
  selector: 'app-confirmemail',
  templateUrl: './confirmemail.component.html',
  styleUrls: ['./confirmemail.component.scss'],
  providers: [MessageService]
})
export class ConfirmemailComponent extends BaseComponent implements OnInit {

  showSuccess: boolean;
  showError: boolean;

  constructor(
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    messageService: MessageService,
    sharedService: SharedService
  ) {
    super(messageService, sharedService);
    this.showSuccess = false;

    this.activatedRoute.queryParams.subscribe(params => {
      this.accountService.confirmEmail({
        id: +params.userId,
        token: params.token
      })
        .pipe(super.start(), super.end())
        .toPromise()
        .then(() => this.showSuccess = true)
        .catch(error => super.handleError(error))
        .then(() => this.showError = true);
    });
  }

  ngOnInit() {
  }

}
