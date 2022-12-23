import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonarsDisplayComponent } from './donars-display.component';

describe('DonarsDisplayComponent', () => {
  let component: DonarsDisplayComponent;
  let fixture: ComponentFixture<DonarsDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonarsDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DonarsDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
