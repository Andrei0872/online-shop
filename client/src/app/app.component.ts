import { Component } from '@angular/core';
import { map } from 'rxjs';
import { OrderService } from './order/order.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  get nrProductsInCart$ () {
    return this.orderService.cart$.pipe(
      map(products => products.reduce((acc, p) => p.quantity + acc, 0))
    );
  }

  constructor (private orderService: OrderService) {}
}
