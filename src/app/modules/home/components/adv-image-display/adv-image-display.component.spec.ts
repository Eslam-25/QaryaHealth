import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AdvImageDisplayComponent } from './adv-image-display.component';

describe('AdvImageDisplayComponent', () => {
  let component: AdvImageDisplayComponent;
  let fixture: ComponentFixture<AdvImageDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AdvImageDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AdvImageDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
