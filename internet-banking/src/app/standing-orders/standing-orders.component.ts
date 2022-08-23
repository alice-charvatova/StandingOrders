import { Component, OnInit } from '@angular/core';
import { StandingOrderService } from './services/standing-order.service';
import { StandingOrders } from '../data/standing-orders';
import { tap, mergeMap, map, take } from 'rxjs/operators';
import { of } from 'rxjs';
import { AuthorizationService } from '../authorization/authorization.service';

@Component({
  selector: 'app-standing-orders',
  templateUrl: './standing-orders.component.html',
  styleUrls: ['./standing-orders.component.scss'],
  providers: [ StandingOrderService, AuthorizationService ]
})

export class StandingOrdersComponent implements OnInit {

  pageTitle: string = 'Standing orders list';
  standingOrders: StandingOrders[] = [];
  errorMessage: string;
  totalAmount: number;
  hoveredIndex: number;

  constructor(private standingOrderService: StandingOrderService,
    private authorizationService: AuthorizationService) {}

  ngOnInit() { 
    this.standingOrderService.getObjects("api/standingOrder")
      .pipe(
        tap(standingOrders => this.standingOrders = standingOrders
          .sort((o1,o2) => (o1.nextRealizationDate < o2.nextRealizationDate) ? -1 : 1)),
        //tap(standingOrders => console.log(standingOrders)),
        //tap(standingOrders => console.table(standingOrders)),
        tap(standingOrders => this.totalAmount = 
          standingOrders
            .map(x => x.amount)
            .reduce((accumulator, currentValue) => accumulator + currentValue))
      ).subscribe();
  }

  deleteOrder(order: StandingOrders) {
    of(order)
    .pipe(
        mergeMap(order => this.authorizationService.authorize()
          .pipe(
            map(token => ({order: order, token: token}))
          )
        ),
        mergeMap(({order, token}) => this.standingOrderService.deleteOrder(order.standingOrderId, token)),
        tap(() => window.location.reload()),
        take(1)
    ).subscribe();
  }
}