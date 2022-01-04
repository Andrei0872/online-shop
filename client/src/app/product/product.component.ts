import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { AuthService } from '../auth/auth.service';
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
    private authService: AuthService
  ) { }

  get products$ (): Observable<Product[]> {
    return this.productService.products$;
  }

  get isCurrentUserAdmin () {
    return this.authService.isCurrentUserAdmin;
  }

  ngOnInit(): void {
  }

  onProductClick (product: Product) {
    this.orderService.addProductToCart(product);
  }
}
