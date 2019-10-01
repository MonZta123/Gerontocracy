import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../shared/components/base/base.component';
import { MessageService } from 'primeng/api';
import { SharedService } from '../../shared/services/shared.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import { User } from '../models/user';
import { Thread } from '../models/thread';
import { Vorfall } from '../models/vorfall';
import { Post } from '../models/post';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss'],
  providers: [MessageService]
})
export class UserComponent extends BaseComponent implements OnInit {

  user: User;
  userNotFound: boolean;

  constructor(
    messageService: MessageService,
    sharedService: SharedService,
    private router: Router,
    private userService: UserService,
    private activatedRoute: ActivatedRoute) {
    super(messageService, sharedService);
  }

  ngOnInit() {
    this.activatedRoute.params.subscribe(n => {
      if (n.name) {
        this.handleUserPageResponse(this.userService.getUserByName(n.name));
      } else {
        this.handleUserPageResponse(this.userService.getUserByClaims());
      }
    });
  }

  navigateToThread(thread: Thread): void {
    this.router.navigate(['board', 'new', thread.id]);
  }

  navigateToAffair(affair: Vorfall): void {
    this.router.navigate(['affair', 'new', affair.id]);
  }

  navigateToPost(post: Post): void {
    this.router.navigate([`board/new/${post.threadId}`], { fragment: post.id.toString() });
  }

  private handleUserPageResponse(response: Observable<User>) {
    response.toPromise()
      .then(n => this.user = n)
      .then(() => console.log(this.user))
      .catch(error => { });
  }

}
