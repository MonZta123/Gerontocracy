import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MatDialog } from '@angular/material';
import { AccountService } from '../../services/account.service';
import { Login } from '../../models/login';
import { MessageBoxComponent } from '../../modules/shared/components/message-box/message-box.component';
import { IconType } from '../../modules/shared/models/icon-type';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss', '../../geronto-global-styles.scss']
})
export class LoginComponent implements OnInit {
  @ViewChild('loginErrorTitle') loginErrorTitle: ElementRef;
  @ViewChild('loginErrorContent') loginErrorContent: ElementRef;

  constructor(
    private formBuilder: FormBuilder,
    private dialogRef: MatDialogRef<LoginComponent>,
    private dialog: MatDialog,
    private accountService: AccountService
  ) { }

  public loginForm: FormGroup;

  isLoginIn = false;

  ngOnInit(): void {
    this.buildLoginForm();
  }

  loginUser(evt: any) {
    if (this.loginForm.valid) {
      this.isLoginIn = true;
      this.loginForm.disable();

      const temp = this.loginForm.value;
      const data: Login = {
        name: temp.name,
        password: temp.pass,
        rememberMe: temp.remember
      };

      this.accountService.loginUser(data)
        .toPromise()
        .then(() => {
          this.isLoginIn = false;
          this.loginForm.enable();
          this.dialogRef.close(true);
        })
        .catch(() =>
          this.dialog.open(MessageBoxComponent,
            {
              disableClose: true,
              width: '400px',
              data: {
                title: this.loginErrorTitle.nativeElement.innerText,
                message: this.loginErrorContent.nativeElement.innerText,
                icon: IconType.Error
              }
            })
            .afterClosed()
            .toPromise()
            .then(() => this.isLoginIn = false)
            .then(() => this.loginForm.enable())
        );
    }
  }

  private buildLoginForm() {
    this.loginForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30), Validators.pattern('[a-zA-Z0-9]*')]],
      pass: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(30)]],
      remember: [false],
    });
  }
}
