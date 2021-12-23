import { Component, OnInit } from '@angular/core';
import { OrderService } from './order.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrls: ['./order.component.scss']
})
export class OrderComponent implements OnInit {

  constructor (private orderService: OrderService) { }

  get orders$ () {
    return this.orderService.orders$;
  }

  ngOnInit(): void {
  }

}
