import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskviewComponent } from './taskview.component';

describe('TaskviewComponent', () => {
  let component: TaskviewComponent;
  let fixture: ComponentFixture<TaskviewComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaskviewComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
