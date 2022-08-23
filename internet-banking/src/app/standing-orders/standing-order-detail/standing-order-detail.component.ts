import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { StandingOrderService } from '../services/standing-order.service';
import { StandingOrder } from '../../data/standing-order';
import { ConstantSymbolDialogComponent } from './constantSymbolDialog/constant-symbol-dialog.component';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { tap, map, mergeMap, filter, take } from 'rxjs/operators';
import { of } from 'rxjs';
import { MatDialog, MatDialogConfig } from '@angular/material/dialog';
import { IbanFormatPipe } from '../../pipes/iban-format.pipe';
import { SpacePipe } from '../../pipes/space.pipe';
import { AuthorizationService } from '../../authorization/authorization.service';
import { validateDateIsLaterThanToday } from 'src/app/custom-validators/custom-date-validator.validator';

@Component({
  selector: 'app-order-detail',
  templateUrl: './standing-order-detail.component.html',
  styleUrls: ['./standing-order-detail.component.scss'],
  providers: [ StandingOrderService, AuthorizationService, IbanFormatPipe, SpacePipe ]
})
export class StandingOrderDetailComponent implements OnInit {

  showForm: boolean = false;
  errorMessage: string;
  orderForm: FormGroup;
  isEditMode: boolean = false;
  submitted: boolean;

  constructor(private route: ActivatedRoute, 
    private router: Router, 
    private standingOrderService: StandingOrderService,
    private dialog: MatDialog, 
    private ibanPipe: IbanFormatPipe, 
    private spacePipe: SpacePipe,
    private authorizationService: AuthorizationService
    ) { }

  goBack() {
    this.router.navigate(['/trvale-prikazy']);
  }

  ngOnInit() {
    this.submitted = false;
    this.orderForm = new FormGroup({
      standingOrderId: new FormControl(),
      name: new FormControl('', [Validators.required]),
      accountNumber: new FormControl('', [Validators.required, Validators.pattern('^[A-Z]{2}[0-9]{2} ?[0-9]{4} ?[0-9]{4} ?[0-9]{4} ?[0-9]{4} ?[0-9]{4}$')]),
      amount: new FormControl('', [Validators.required, Validators.pattern('^[0-9]{1,10}((\.|\,)?[0-9]{1,2})?$')]),
      variableSymbol: new FormControl(),
      constantSymbol: new FormControl(),
      specificSymbol: new FormControl(),
      note: new FormControl(),
      validFrom: new FormControl('', [Validators.required, validateDateIsLaterThanToday]),
      periodicityForm: new FormGroup({
        intervalId: new FormControl(),
        intervalSpecification: new FormControl(),
      }) 
    }); 
    let id = +this.route.snapshot.paramMap.get('id');  // pokud id neobsahuje cislo, vraci NaN
    if (Number.isFinite(id)) {  // ciselne id bylo soucasti URL, takze se jedna o editaci (nikoliv o vytvareni) trvaleho prikazu => napln formular daty
      this.isEditMode = true;
      this.standingOrderService.getOrder(id)
      .pipe(
          tap(standingOrder => this.displayValue(standingOrder))
      ).subscribe();
    }
  }

  displayValue(standingOrder) {
    if (this.orderForm) {
      this.orderForm.reset();
    }
    this.orderForm.patchValue({
      ...standingOrder,
      accountNumber: this.ibanPipe.transform(standingOrder.accountNumber),
      validFrom: standingOrder.validFrom.slice(0, 10),
      periodicityForm: {
        intervalId: standingOrder.intervalId,
        intervalSpecification: standingOrder.intervalSpecification
      }   
    });
  }

  openConstantSymbolDialog() {
    const dialogConfig = new MatDialogConfig();
    dialogConfig.autoFocus = true;
    const dialogRef = this.dialog.open(ConstantSymbolDialogComponent, dialogConfig);
    dialogRef.afterClosed().subscribe(result => 
      {if (result != null) {
        this.orderForm.patchValue({
          constantSymbol: result
        })
      }}
    );
  }

  saveForm() {
    this.submitted = true;
    of(this.orderForm)
      .pipe(
        filter(data => data.valid === true),
        mergeMap(form => this.authorizationService.authorize()
          .pipe(
            map(token => ({form: form, token: token}))
          )
        ),
        mergeMap(({form, token}) => this.isEditMode ? 
          this.standingOrderService.updateOrder(this.getValues(form), token) : 
          this.standingOrderService.createOrder(this.getValues(form), token)),
        tap(() => this.goBack()),
        take(1)
        )
      .subscribe();

  }

  getValues(form: FormGroup) : StandingOrder{
    const standingOrder = {
      ...form.value, 
      amount: parseFloat(form.value.amount),
      accountNumber: this.spacePipe.transform(form.value.accountNumber),
      intervalId: form.value.periodicityForm.intervalId,                              
      intervalSpecification: +form.value.periodicityForm.intervalSpecification };   
    delete standingOrder.periodicityForm
    return standingOrder;
  }
}

