import { Component, OnInit, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../../services/account.service';
import { MessageService } from '../../modules/shared/services/message.service';

@Component({
  selector: 'app-confirmemail',
  templateUrl: './confirmemail.component.html',
  styleUrls: ['./confirmemail.component.scss']
})
export class ConfirmemailComponent implements OnInit {
  @ViewChild('emailConfirmedTitle') emailConfirmedTitle: ElementRef;
  @ViewChild('emailConfirmedMessage') emailConfirmedMessage: ElementRef;
  @ViewChild('emailErrorTitle') emailErrorTitle: ElementRef;
  @ViewChild('emailErrorMessage') emailErrorMessage: ElementRef;

  constructor(
    private router: Router,
    private accountService: AccountService,
    private activatedRoute: ActivatedRoute,
    private messageService: MessageService,
  ) {

    this.activatedRoute.queryParams.subscribe(params => {
      this.accountService.confirmEmail({
        id: +params.userId,
        token: params.token
      })
        .toPromise()
        .then(() => this.messageService.showInfoBox(
          this.emailConfirmedTitle.nativeElement.innerText,
          this.emailConfirmedMessage.nativeElement.innerText)
          .afterClosed()
          .toPromise().then(() => this.router.navigate(['/'])
          ))
        .catch(() => this.messageService.showAlertBox(
          this.emailErrorTitle.nativeElement.innerText,
          this.emailErrorMessage.nativeElement.innerText));
    });
  }

  ngOnInit() {
  }

}
