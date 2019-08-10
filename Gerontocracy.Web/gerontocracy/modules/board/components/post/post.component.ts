import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Post } from '../../models/post';
import { LikeType } from '../../models/like-type';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { LoginComponent } from '../../../../components/login/login.component';
import { BoardService } from '../../services/board.service';
import { SharedAccountService } from '../../../shared/services/shared-account.service';
import { MatDialog } from '@angular/material';

@Component({
  selector: 'app-post',
  templateUrl: './post.component.html',
  styleUrls: ['./post.component.scss', '../../../../geronto-global-styles.scss']
})
export class PostComponent implements OnInit {

  @Input() data: Post;
  @Output() reply: EventEmitter<void> = new EventEmitter<void>();

  constructor(
    private formBuilder: FormBuilder,
    private boardService: BoardService,
    private sharedAccountService: SharedAccountService,
    private dialog: MatDialog
  ) { }

  showReplyWindow: boolean;
  formGroup: FormGroup;
  isLoadingData: boolean;

  LikeType = LikeType;

  ngOnInit() {
    this.isLoadingData = false;
    this.showReplyWindow = false;

    this.formGroup = this.formBuilder.group({
      reply: [null, [Validators.required]]
    });
  }

  likeToColor = (type: LikeType): string => (this.data && this.data.userLike === type) ? 'accent' : '';

  like(type: LikeType) {
    this.sharedAccountService.isLoggedIn().toPromise().then(n => {
      if (n) {
        let newLikeType: LikeType;
        const ul = this.data.userLike;

        if (ul === LikeType.like) {
          if (type === LikeType.like) {
            this.data.likes--;
          } else {
            this.data.likes--;
            this.data.dislikes++;
            newLikeType = LikeType.dislike;
          }
        }

        if (ul === LikeType.dislike) {
          if (type === LikeType.like) {
            this.data.likes++;
            this.data.dislikes--;
            newLikeType = LikeType.like;
          } else {
            this.data.dislikes--;
          }
        }

        if (ul === null || ul === undefined) {
          if (type === LikeType.like) {
            this.data.likes++;
            newLikeType = LikeType.like;
          } else {
            this.data.dislikes++;
            newLikeType = LikeType.dislike;
          }
        }

        this.boardService
          .like(this.data.id, newLikeType)
          .toPromise()
          .then(() => this.data.userLike = newLikeType);

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

  onReply(evt: any) {
    this.sharedAccountService.isLoggedIn().toPromise().then(r => {
      if (r) {
        if (this.formGroup.valid) {
          this.boardService.reply(this.data.id, this.formGroup.controls.reply.value)
            .toPromise()
            .then(() => this.reply.emit());
        }
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

  showUser(id: number) {

  }
}
