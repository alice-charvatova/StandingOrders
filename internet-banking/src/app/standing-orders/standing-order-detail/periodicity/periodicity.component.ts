import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { tap, map } from 'rxjs/operators';
import { StandingOrderService } from '../../services/standing-order.service';
import { INTERVALS_URL } from 'src/app/constants/urls';

@Component({
  selector: 'app-periodicity',
  templateUrl: './periodicity.component.html',
  styleUrls: ['./periodicity.component.scss'],
  providers: [ StandingOrderService ]

})
export class PeriodicityComponent implements OnInit {
  
  monthly = [];
  intervals = [];
  weekly = [
    {name: "Pondelok", value: 1},
    {name: "Utorok", value: 2},
    {name: "Streda", value: 3},
    {name: "Štvrtok", value: 4},
    {name: "Piatok", value: 5},
    {name: "Sobota", value: 6},
    {name: "Nedeľa", value: 7}
  ];
  errorMessage: string;
  @Input() periodicityForm: FormGroup;
  intervalSelect;

  constructor(private standingOrderService: StandingOrderService) {}

  ngOnInit() {
    //debugger;
    this.fillMonthlyArray();
    this.standingOrderService.getObjects(INTERVALS_URL)
      .pipe(
          tap(intervals => this.intervals = intervals)
      ).subscribe(); 
  }

  fillMonthlyArray() {
    for(let i = 1; i <= 31; i++) {
      this.monthly.push({name: i.toString(), value: i});
    }
  }

  ngOnChanges() {
    this.refresh();
  }

  refresh() {
      this.periodicityForm.get('intervalId').valueChanges
        .pipe(
          map(x => {
            this.intervalSelect = x;
            this.periodicityForm.patchValue({
                intervalSpecification: x == 1 ? 0 : 1
            });
          })
        ).subscribe();
  }
}
