import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StandingOrderDetailComponent } from './standing-order-detail.component';

describe('StandingOrderDetailComponent', () => {
  let component: StandingOrderDetailComponent;
  let fixture: ComponentFixture<StandingOrderDetailComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StandingOrderDetailComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StandingOrderDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
