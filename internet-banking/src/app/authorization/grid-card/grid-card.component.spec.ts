import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { GridCardDialogComponent } from './grid-card.component';

describe('GridCardComponent', () => {
  let component: GridCardDialogComponent;
  let fixture: ComponentFixture<GridCardDialogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ GridCardDialogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(GridCardDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
