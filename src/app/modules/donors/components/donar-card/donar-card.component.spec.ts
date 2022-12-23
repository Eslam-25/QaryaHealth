import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DonarCardComponent } from './donar-card.component';

describe('DonarCardComponent', () => {
  let component: DonarCardComponent;
  let fixture: ComponentFixture<DonarCardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DonarCardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DonarCardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
