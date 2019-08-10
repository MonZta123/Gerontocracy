import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { VorfallDetail } from '../../models/vorfall-detail';
import { VoteType } from '../../models/vote-type';
import { AffairService } from '../../services/affair.service';
import { SharedAccountService } from '../../../shared/services/shared-account.service';
import { LoginComponent } from '../../../../components/login/login.component';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-detail',
  templateUrl: './detail.component.html',
  styleUrls: ['./detail.component.scss', '../../../../geronto-global-styles.scss']
})
export class DetailComponent implements OnInit {

  @Input() data: VorfallDetail;

  @Output() fullscreen: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Output() close: EventEmitter<any> = new EventEmitter<any>();

  isFullscreen: boolean;
  isLoading: boolean;

  public VoteType = VoteType;

  displayedColumns = ['id', 'url', 'zusatz', 'legitimitaet'];

  constructor(
    private affairService: AffairService,
    private sharedAccountService: SharedAccountService,
    private dialog: MatDialog) { }

  ngOnInit() {
    this.isFullscreen = false;
    this.isLoading = false;
  }

  toggleSize(evt: boolean) {
    this.isFullscreen = evt;
    this.fullscreen.emit(this.isFullscreen);
  }

  voteToColor = (type: VoteType): string => (this.data && this.data.userVote === type) ? 'accent' : '';

  onVoteClicked(type: VoteType): void {

    this.sharedAccountService.isLoggedIn().toPromise().then(n => {
      if (n) {

        let newVoteType: VoteType;
        if (this.data.userVote !== type) {
          newVoteType = type;
        }

        this.affairService
          .vote(this.data.id, newVoteType)
          .toPromise()
          .then(() => this.data.userVote = newVoteType);

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
}
