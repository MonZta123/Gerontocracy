import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AddSourceDialogComponent } from './add-source-dialog.component';

describe('AddSourceDialogComponent', () => {
  let component: AddSourceDialogComponent;
  let fixture: ComponentFixture<AddSourceDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ AddSourceDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AddSourceDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
