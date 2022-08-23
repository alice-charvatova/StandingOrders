import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { throwError } from 'rxjs';

@Injectable({ providedIn: 'root' })

export class ErrorHandlingService{

    errorMessage: string = '';
    
    public handleError(err: HttpErrorResponse) {
        if (err.error instanceof ErrorEvent) {
          this.errorMessage = `An error occured: ${err.error.message}`;
        }
        else {
          this.errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
        }
        return throwError(this.errorMessage);
    } 
}