import { Injectable } from '@angular/core';
import { MessageBoxComponent } from '../components/message-box/message-box.component';
import { MatDialog, MatSnackBar, MatDialogRef } from '@angular/material';
import { IconType } from '../models/icon-type';
import { YesNoBoxComponent } from '../components/yes-no-box/yes-no-box.component';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  constructor(
    private dialog: MatDialog,
    private matSnackBar: MatSnackBar
  ) { }

  public showInfoBox = (titel: string, message: string) => this.showBox(titel, message, IconType.Info);
  public showAlertBox = (titel: string, message: string) => this.showBox(titel, message, IconType.Error);
  public showWarningBox = (titel: string, message: string) => this.showBox(titel, message, IconType.Warning);

  public showBox(title: string, message: string, type: IconType): MatDialogRef<MessageBoxComponent> {
    return this.dialog.open(MessageBoxComponent, {
      disableClose: true,
      hasBackdrop: true,
      width: '512px',
      data: { title, message, icon: type }
    });
  }

  public showConfirmationBox(title: string, message: string, type: IconType, yesText: string, cancelText: string): MatDialogRef<YesNoBoxComponent> {
    return this.dialog.open(YesNoBoxComponent, {
      hasBackdrop: true,
      disableClose: true,
      width: '512px',
      data: { title, message, icon: type, yesText, cancelText }
    });
  }

  public showSnackbar(message: string, action: string) {
    this.matSnackBar.open(message, action, {
      duration: 4000,
      verticalPosition: 'bottom',
      horizontalPosition: 'end',
    });
  }
}
