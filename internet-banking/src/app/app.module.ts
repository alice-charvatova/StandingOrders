import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { StandingOrdersComponent } from './standing-orders/standing-orders.component';
import { StandingOrderDetailComponent } from './standing-orders/standing-order-detail/standing-order-detail.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { HttpClientModule } from '@angular/common/http';
import { DateFormatPipe } from './pipes/date-format.pipe'; 
import { CurrencyFormatPipe } from './pipes/currency-format.pipe';
import { IbanFormatPipe } from './pipes/iban-format.pipe';
import { ReactiveFormsModule } from '@angular/forms';
import { PeriodicityComponent } from './standing-orders/standing-order-detail/periodicity/periodicity.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ConstantSymbolDialogComponent } from './standing-orders/standing-order-detail/constantSymbolDialog/constant-symbol-dialog.component';
import { SpacePipe } from './pipes/space.pipe';
import { GridCardDialogComponent } from './authorization/grid-card/grid-card.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { FormsModule } from '@angular/forms';
import { DecimalPipe } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    StandingOrdersComponent,
    StandingOrderDetailComponent,
    DateFormatPipe,
    CurrencyFormatPipe,
    IbanFormatPipe,
    SpacePipe,
    PeriodicityComponent,
    ConstantSymbolDialogComponent,
    GridCardDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
    MatProgressSpinnerModule,
    ReactiveFormsModule,
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    FormsModule 
  ],
  providers: [DecimalPipe],
  bootstrap: [AppComponent],
  entryComponents: [
    ConstantSymbolDialogComponent, 
    GridCardDialogComponent
  ]
})

export class AppModule { }
