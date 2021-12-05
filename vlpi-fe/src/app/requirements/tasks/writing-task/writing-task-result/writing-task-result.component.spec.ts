import { ComponentFixture, TestBed } from '@angular/core/testing';

import { WritingTaskResultComponent } from './writing-task-result.component';

describe('WritingTaskResultComponent', () => {
  let component: WritingTaskResultComponent;
  let fixture: ComponentFixture<WritingTaskResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ WritingTaskResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(WritingTaskResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
