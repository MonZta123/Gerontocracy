import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedPartyService } from './services/shared-party.service';
import { SharedAccountService } from './services/shared-account.service';
import { SharedBoardService } from './services/shared-board.service';
import { LockedInterceptorService } from './services/locked-interceptor.service';

import { StringTrimPipe } from './pipes/string-trim.pipe';
import { UrlPipe } from './pipes/url.pipe';
import { ToastModule } from 'primeng/toast';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { BaseComponent } from './components/base/base.component';
import { BlockUIModule } from 'primeng/blockui';

@NgModule({
  declarations: [
    StringTrimPipe,
    UrlPipe,
    BaseComponent,
  ],
  exports: [
    StringTrimPipe,
    UrlPipe,
    BaseComponent
  ],
  imports: [
    CommonModule,
    ToastModule,
    BlockUIModule
  ],
  entryComponents: [
  ],
  providers: [
    SharedAccountService,
    SharedPartyService,
    SharedBoardService,
    { provide: HTTP_INTERCEPTORS, useClass: LockedInterceptorService, multi: true },
  ],
})
export class SharedModule { }
