import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HomeDisplayComponent } from './home-display.component';

describe('HomeDisplayComponent', () => {
  let component: HomeDisplayComponent;
  let fixture: ComponentFixture<HomeDisplayComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HomeDisplayComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HomeDisplayComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
