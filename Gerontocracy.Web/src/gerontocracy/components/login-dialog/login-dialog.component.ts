import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Message, DynamicDialogRef, DynamicDialogConfig } from 'primeng/api';
import { AccountService } from '../../services/account.service';
import { Login } from '../../models/login';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login-dialog',
  templateUrl: './login-dialog.component.html',
  styleUrls: ['./login-dialog.component.scss']
})
export class LoginDialogComponent implements OnInit {

  loginForm: FormGroup;
  loginErrors: Message[];
  isLogingIn: boolean;
  rememberMe: boolean;
  checkboxDisabled: boolean;

  constructor(
    private dialogRef: DynamicDialogRef,
    private dialogConfig: DynamicDialogConfig,
    private formBuilder: FormBuilder,
    private accountService: AccountService) { }

  ngOnInit() {
    this.checkboxDisabled = false;
    this.rememberMe = false;
    this.isLogingIn = false;
    this.loginErrors = [];

    this.buildLoginForm();
  }

  checkboxChanged(value: boolean) {
    this.rememberMe = value;
  }

  loginUser() {
    if (this.loginForm.valid) {
      this.isLogingIn = true;
      this.loginForm.disable();
      this.checkboxDisabled = true;

      const temp = this.loginForm.value;
      const data: Login = {
        name: temp.name,
        password: temp.pass,
        rememberMe: this.rememberMe
      };

      this.accountService.loginUser(data)
        .toPromise()
        .then(() => {
          this.isLogingIn = false;
          this.closeLoginForm(true);
        })
        .catch(m => {
          if (m.status === 403) {
            this.loginErrors = [{ severity: 'error', summary: 'Fehler', detail: m.error.Message }];
          } else {
            this.loginErrors = [{ severity: 'error', summary: 'Fehler', detail: 'Login fehlgeschlagen, bitte die Zugangsdaten prüfen!' }];
          }
          this.isLogingIn = false;
          this.checkboxDisabled = false;
          this.loginForm.enable();
        });
    }
  }

  closeLoginForm(reloadScreen: boolean): void {
    this.dialogRef.close(reloadScreen);
  }

  private buildLoginForm() {
    this.loginForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30), Validators.pattern('[a-zA-Z0-9]*')]],
      pass: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(30)]],
    });
  }

}
