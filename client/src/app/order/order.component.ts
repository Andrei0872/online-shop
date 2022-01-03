import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';
import { Order } from './order.model';
import { OrderService } from './order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {
  orders$: Observable<Order[]>;

  constructor (private orderService: OrderService, private route: ActivatedRoute) {
    const userId = this.route.snapshot.paramMap.get('userId');
    this.orders$ = userId ? orderService.fetchOrdersOfUser(+userId) : this.orderService.orders$;
  }

  ngOnInit(): void {
  }

}
