import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrderRoutingModule } from './order-routing.module';
import { OrderComponent } from './order.component';
import { SingleOrderComponent } from './single-order/single-order.component';
import { ComputeTotalPricePipe } from './compute-total-price.pipe';


@NgModule({
  declarations: [
    OrderComponent,
    SingleOrderComponent,
    ComputeTotalPricePipe
  ],
  imports: [
    CommonModule,
    OrderRoutingModule
  ]
})
export class OrderModule { }
