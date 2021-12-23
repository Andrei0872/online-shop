import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OrderComponent } from './order.component';
import { SingleOrderComponent } from './single-order/single-order.component';

const routes: Routes = [
  {
    path: '',
    component: OrderComponent
  },
  {
    path: ':idOrCurrent',
    component: SingleOrderComponent,
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OrderRoutingModule { }
