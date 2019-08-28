import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { UnbanUserDialogComponent } from './unban-user-dialog.component';

describe('UnbanUserDialogComponent', () => {
  let component: UnbanUserDialogComponent;
  let fixture: ComponentFixture<UnbanUserDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ UnbanUserDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(UnbanUserDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
