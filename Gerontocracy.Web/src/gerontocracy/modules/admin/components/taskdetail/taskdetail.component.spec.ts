import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TaskdetailComponent } from './taskdetail.component';

describe('TaskdetailComponent', () => {
  let component: TaskdetailComponent;
  let fixture: ComponentFixture<TaskdetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TaskdetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TaskdetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
