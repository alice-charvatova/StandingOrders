import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of, Subject } from 'rxjs';
import { catchError, mergeMap, filter, retryWhen, tap } from 'rxjs/operators';
import { GridCardDialogComponent } from './grid-card/grid-card.component';
import { HEADERS } from '../constants/headers';
import { ErrorHandlingService } from '../error-handling/error-handling.service';
import { MatDialog } from '@angular/material/dialog';
import { DialogData } from './grid-card/dialog-data';

@Injectable()

export class AuthorizationService{
 
    private gridCardUrl = 'api/grid-card';

    constructor(private http: HttpClient,
        private errorHandler: ErrorHandlingService,
        private dialog: MatDialog ) {}

    public authorize(): Observable<string>{
        let submitted = false;
        return of(null)
            .pipe(
                mergeMap(() => this.getCoordinates()),
                mergeMap((coordinates) => this.showDialog(coordinates, submitted)),
                tap(authData => submitted = authData.submitted),
                filter(authData => authData.submitted === true),
                mergeMap(authData => this.verify(authData)),
                retryWhen(errors => errors
                    .pipe(
                    tap(errorStatus => {
                        if (errorStatus == 400) {
                        throw errorStatus;
                        }
                    })
                    )
                )        
            )
    }

    private getCoordinates(): Observable<number>{
        const url = `${this.gridCardUrl}/init`;
        return this.http.get<number>(url).pipe(catchError(this.errorHandler.handleError));
    }    
    
    private showDialog(coordinates: number, dialogSubmitted: boolean) {
        let pinCode: number;
        let subject = new Subject<DialogData>();
        
        const dialogRef = this.dialog.open(GridCardDialogComponent, {
            width: '500px',
            data: {pin: pinCode, coordinate: coordinates, submitted: dialogSubmitted}
        });

        dialogRef.afterClosed().subscribe(result => { 
            let authData: DialogData;
            if (result) {
                pinCode = result.pin;
                authData = {pinCode: pinCode, coordinate: coordinates, submitted: true};   
            } 
            else{
                authData = {pinCode: null, coordinate: null, submitted: false};   
            }
            subject.next(authData);        
        });
        return subject.asObservable();
    }
      
    private verify(authData: DialogData): Observable<string> {
        const url = `${this.gridCardUrl}/validate`;
        return this.http.post<string>(url, authData, {headers: HEADERS})
            .pipe(catchError(this.errorHandler.handleError))
    }
}