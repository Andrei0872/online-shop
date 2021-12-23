import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { filter, map, Observable, tap } from 'rxjs';
import { CurrentOrder } from '../order.model';
import { OrderService } from '../order.service';

@Component({
  selector: 'app-single-order',
  templateUrl: './single-order.component.html',
  styleUrls: ['./single-order.component.scss']
})
export class SingleOrderComponent implements OnInit {
  isInspectingExistingOrder = false;
  order$!: Observable<CurrentOrder | null>;

  get headerMessage () {
    return this.isInspectingExistingOrder
    ? 'Inspecting the order with the ID '
    : 'Inspecting the cart\'s content';
  }

  constructor (
    private orderService: OrderService,
    private route: ActivatedRoute,
  ) {
    this.initializeComponent();
  }

  ngOnInit(): void {
  }

  private initializeComponent () {
    let { idOrCurrent } = this.route.snapshot.params;
    idOrCurrent = +idOrCurrent;

    this.isInspectingExistingOrder = !isNaN(idOrCurrent);

    const id = this.isInspectingExistingOrder && idOrCurrent || null;
    if (!id) {
      return;
    }

    this.order$ = this.orderService.currentOrder$.pipe(
      map(o => o === null || +o.id !== +id ? (this.orderService.fetchOrder(id), false) : o),
      filter(Boolean),
    )
  }
}
