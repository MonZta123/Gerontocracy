import { Component, OnInit } from '@angular/core';
import { BaseComponent } from '../../shared/components/base/base.component';
import { MessageService } from 'primeng/api';
import { SharedService } from '../../shared/services/shared.service';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { UserService } from '../services/user.service';
import { User } from '../models/user';

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

  private handleUserPageResponse(response: Observable<User>) {
    response.toPromise()
      .then(n => this.user = n)
      .then(() => console.log(this.user))
      .catch(error => { });
  }

}
