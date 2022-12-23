import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DatePickerArabicComponent } from './date-picker-arabic.component';

describe('DatePickerArabicComponent', () => {
  let component: DatePickerArabicComponent;
  let fixture: ComponentFixture<DatePickerArabicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DatePickerArabicComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DatePickerArabicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
