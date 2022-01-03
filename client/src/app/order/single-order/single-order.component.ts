import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { filter, map, Observable, tap } from 'rxjs';
import { CurrentOrder, OrderProduct } from '../order.model';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-single-order',
  templateUrl: './single-order.component.html',
  styleUrls: ['./single-order.component.scss']
})
export class SingleOrderComponent implements OnInit {
  orderId!: number;
  order$!: Observable<CurrentOrder | null>;

  get isInspectingExistingOrder () {
    return this.orderId !== -1;
  }

  get headerMessage () {
    return this.isInspectingExistingOrder
    ? `Inspecting the order with the ID ${this.orderId}`
    : 'Inspecting the cart\'s content';
  }

  constructor (
    private orderService: OrderService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    this.initializeComponent();
  }

  ngOnInit(): void {
  }

  private initializeComponent () {
    this.route.params.subscribe(({ idOrCurrent }) => {
      const existingOrderId = !isNaN(+idOrCurrent) && +idOrCurrent || null;
      this.orderId = existingOrderId || -1;
  
      this.order$ = existingOrderId
        ? this.orderService.currentOrder$.pipe(
          map(o => o === null || +o.id !== +existingOrderId ? (this.orderService.fetchOrder(existingOrderId), false) : o),
          filter(Boolean),
        )
        : this.orderService.cart$.pipe(
          // Converting into `CurrentOrder`.
          map(products => ({ id: -1, products, totalPrice: 0 }))
        )
    });
  }

  submitOrder (products: OrderProduct[]) {
    this.orderService.submitOrder(products)
      .subscribe(() => {
        this.orderService.markOrdersAsDirty();
        this.router.navigateByUrl("/orders");
      });
  }
}
