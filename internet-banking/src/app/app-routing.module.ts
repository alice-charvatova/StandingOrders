import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StandingOrdersComponent } from './standing-orders/standing-orders.component';
import { StandingOrderDetailComponent } from './standing-orders/standing-order-detail/standing-order-detail.component';

const ROUTES : Routes = [
  { path: 'trvale-prikazy', component: StandingOrdersComponent },
  { path: 'trvale-prikazy/:id', component: StandingOrderDetailComponent },
  { path: 'trvale-prikazy/novy', component: StandingOrderDetailComponent },
  { path: '', redirectTo: 'trvale-prikazy', pathMatch: 'full' },
  { path: '**', redirectTo: 'trvale-prikazy', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(ROUTES) ],
  exports: [RouterModule]
})

export class AppRoutingModule { }


