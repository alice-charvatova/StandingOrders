import {Component, OnInit} from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';
import { ConstantSymbols } from 'src/app/data/constant-symbols';
import { tap } from 'rxjs/operators';
import { StandingOrderService } from '../../services/standing-order.service';
import { CONSTANT_SYMBOLS_URL } from 'src/app/constants/urls';

@Component({
    selector: 'constant-symbol-dialog',
    templateUrl: 'constant-symbol-dialog.component.html',
    styleUrls: ['./constant-symbol-dialog.component.scss'],
    providers: [ StandingOrderService ]
  })

export class ConstantSymbolDialogComponent implements OnInit{
  
  constantSymbols: ConstantSymbols[];
  hoveredIndex: number;
  errorMessage: string;
  constantSymbol: string;

  constructor(
    public dialogRef: MatDialogRef<ConstantSymbolDialogComponent>,
    private standingOrderService: StandingOrderService) {}

  ngOnInit() {
    this.standingOrderService.getObjects(CONSTANT_SYMBOLS_URL)
    .pipe(
        tap(constantSymbols => this.constantSymbols = constantSymbols)
    )
    .subscribe();
  }

  cancel() {
    this.dialogRef.close();
  }

  close(value: string) {
      this.dialogRef.close(value);
  }
}