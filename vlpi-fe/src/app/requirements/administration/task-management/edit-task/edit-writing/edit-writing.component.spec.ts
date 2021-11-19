import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditWritingComponent } from './edit-writing.component';

describe('EditWritingComponent', () => {
  let component: EditWritingComponent;
  let fixture: ComponentFixture<EditWritingComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditWritingComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditWritingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
