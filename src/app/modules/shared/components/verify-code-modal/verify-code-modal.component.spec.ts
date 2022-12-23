import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VerifyCodeModalComponent } from './verify-code-modal.component';

describe('VerifyCodeModalComponent', () => {
  let component: VerifyCodeModalComponent;
  let fixture: ComponentFixture<VerifyCodeModalComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VerifyCodeModalComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VerifyCodeModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
