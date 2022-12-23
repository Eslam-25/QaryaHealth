import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MedicalStaffCardComponent } from './medical-staff-card.component';

describe('MedicalStaffCardComponent', () => {
  let component: MedicalStaffCardComponent;
  let fixture: ComponentFixture<MedicalStaffCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MedicalStaffCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MedicalStaffCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
