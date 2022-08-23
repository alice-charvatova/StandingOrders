import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { StandingOrdersComponent } from './standing-orders.component';

describe('StandingOrdersListComponent', () => {
  let component: StandingOrdersComponent;
  let fixture: ComponentFixture<StandingOrdersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ StandingOrdersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(StandingOrdersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
