import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BanUserDialogComponent } from './ban-user-dialog.component';

describe('BanUserDialogComponent', () => {
  let component: BanUserDialogComponent;
  let fixture: ComponentFixture<BanUserDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BanUserDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BanUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
