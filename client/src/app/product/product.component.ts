import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { OrderService } from '../order/order.service';
import { Product } from './product.model';
import { ProductService } from './product.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit {

  constructor (
    private productService: ProductService,
    private orderService: OrderService,
  ) { }

  get products$ (): Observable<Product[]> {
    return this.productService.products$;
  }

  ngOnInit(): void {
  }

  onProductClick (product: Product) {
    this.orderService.addProductToCart(product);
  }
}
