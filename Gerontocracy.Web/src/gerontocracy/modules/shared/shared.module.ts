import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedPartyService } from './services/shared-party.service';
import { SharedAccountService } from './services/shared-account.service';
import { SharedBoardService } from './services/shared-board.service';
import { LockedInterceptorService } from './services/locked-interceptor.service';

import { StringTrimPipe } from './pipes/string-trim.pipe';
import { FullnamePipe } from './pipes/fullname.pipe';
import { UrlPipe } from './pipes/url.pipe';
import { HTTP_INTERCEPTORS } from '@angular/common/http';

@NgModule({
  declarations: [
    StringTrimPipe,
    FullnamePipe,
    UrlPipe,
  ],
  exports: [
    StringTrimPipe,
    FullnamePipe,
    UrlPipe,
  ],
  imports: [
    CommonModule,
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
