import { Component, OnInit, ViewChild, ElementRef, Inject } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialogRef, MatDialog, MAT_DIALOG_DATA } from '@angular/material';
import { AccountService } from '../../services/account.service';
import { Register } from '../../models/register';
import { IconType } from '../../modules/shared/models/icon-type';
import { MessageBoxComponent } from '../../modules/shared/components/message-box/message-box.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss', '../../geronto-global-styles.scss']
})
export class RegisterComponent implements OnInit {
  @ViewChild('registrationTitle') registrationTitle: ElementRef;
  @ViewChild('registrationContent') registrationContent: ElementRef;
  @ViewChild('errorTitle') errorTitle: ElementRef;
  @ViewChild('errorMessage') errorMessage: ElementRef;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService,
    private dialog: MatDialog,
    public dialogRef: MatDialogRef<RegisterComponent>
  ) { }

  registerForm: FormGroup;
  checkUsernameAvailability: boolean;
  checkEmailAvailability: boolean;

  isRegistering = false;

  ngOnInit(): void {
    this.buildRegisterForm();
  }

  private buildRegisterForm() {
    this.registerForm = this.formBuilder.group({
      user: ['', [Validators.required, Validators.minLength(3), Validators.maxLength(30), Validators.pattern('[a-zA-Z0-9]*')]],
      mail: ['', [Validators.required, Validators.email, Validators.maxLength(40)]],
      pass1: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(30)]],
      pass2: ['', [Validators.required, this.passwordConfirming]]
    });
  }

  checkUserExists() {
    if (!!this.registerForm.controls.user.value && !this.registerForm.controls.user.errors) {
      this.checkUsernameAvailability = true;

      this.accountService.getUserExists(this.registerForm.controls.user.value)
        .toPromise()
        .then(n => {

          if (n) {
            this.registerForm.controls.user.setErrors({ alreadyInUse: true });
          }

          this.checkUsernameAvailability = false;
        });
    }
  }

  checkEmailExists() {
    if (!!this.registerForm.controls.mail.value && !this.registerForm.controls.mail.errors) {
      this.checkEmailAvailability = true;

      this.accountService.getEmailExists(this.registerForm.controls.mail.value)
        .toPromise()
        .then(n => {

          if (n) {
            this.registerForm.controls.mail.setErrors({ alreadyInUse: true });
          }

          this.checkEmailAvailability = false;
        });
    }
  }

  registerUser() {
    if (this.registerForm.valid) {
      this.isRegistering = true;
      this.registerForm.disable();

      const temp = this.registerForm.value;
      const data: Register = {
        email: temp.mail,
        name: temp.user,
        password: temp.pass1
      };

      this.accountService.registerUser(data)
        .toPromise()
        .then(() => {

          this.isRegistering = false;
          this.registerForm.enable();
          this.dialog.open(MessageBoxComponent,
            {
              disableClose: true,
              width: '512px',
              data: {
                title: this.registrationTitle.nativeElement.innerText,
                message: this.registrationContent.nativeElement.innerText,
                icon: IconType.Info
              }
            })
            .afterClosed()
            .toPromise()
            .then(() => this.isRegistering = false)
            .then(() => this.registerForm.enable())
            .then(() => this.dialogRef.close());
        })
        .catch(() =>
          this.dialog.open(MessageBoxComponent,
            {
              disableClose: true,
              width: '512px',
              data: {
                title: this.errorTitle.nativeElement.innerText,
                message: this.errorMessage.nativeElement.innerText,
                icon: IconType.Info
              }
            })
            .afterClosed()
            .toPromise()
            .then(() => this.isRegistering = false)
            .then(() => this.registerForm.enable())
        );
    }
  }

  passwordConfirming(control: FormControl) {
    if (!(control.root as FormGroup).controls) {
      return null;
    }

    const pass = (control.root as FormGroup).controls.pass1.value;
    const confirmPass = (control.root as FormGroup).controls.pass2.value;

    return pass === confirmPass ? null : { notSame: true };
  }

}
