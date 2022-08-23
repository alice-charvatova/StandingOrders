import { Injectable } from '@angular/core';
import { StandingOrder } from '../../data/standing-order';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { HEADERS } from '../../constants/headers';
import { ErrorHandlingService } from '../../error-handling/error-handling.service';
import { STANDING_ORDERS_URL } from 'src/app/constants/urls';


@Injectable()

export class StandingOrderService{

    private ordersUrl = STANDING_ORDERS_URL;

    constructor(private http: HttpClient,
      private errorHandler: ErrorHandlingService) {}

    getObjects(url: string): Observable<any> {
      return this.http.get<any>(url).pipe(catchError(this.errorHandler.handleError));
    }

    getOrder(id: number): Observable<StandingOrder> {
      const url = `${this.ordersUrl}/${id}`;
      return this.http.get<StandingOrder>(url).pipe(catchError(this.errorHandler.handleError));
    } 

    updateOrder (order: StandingOrder, token: string): Observable<StandingOrder> {
      const url = `${this.ordersUrl}/${order.standingOrderId}`;
      let custHeaders = {...HEADERS, 'Authorization': token}
      return this.http.put<StandingOrder>(url, order, {headers: custHeaders})
        .pipe(
          map(() => order),
          catchError(this.errorHandler.handleError));
    }

    createOrder (order: StandingOrder, token: string): Observable<StandingOrder> {
      let custHeaders = {...HEADERS, 'Authorization': token}
      return this.http.post<StandingOrder>(this.ordersUrl, order, {headers: custHeaders})
        .pipe(
          map(() => order),
          catchError(this.errorHandler.handleError));
    }    

    deleteOrder(id: number, token: string) {
      const url = `${this.ordersUrl}/${id}`;
      let custHeaders = {...HEADERS, 'Authorization': token}
      return this.http.delete<StandingOrder>(url, {headers: custHeaders}).pipe(catchError(this.errorHandler.handleError));
    } 
}

