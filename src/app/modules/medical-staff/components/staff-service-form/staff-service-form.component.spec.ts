import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StaffServiceFormComponent } from './staff-service-form.component';

describe('StaffServiceFormComponent', () => {
  let component: StaffServiceFormComponent;
  let fixture: ComponentFixture<StaffServiceFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ StaffServiceFormComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(StaffServiceFormComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
