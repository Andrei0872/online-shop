import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { map } from 'rxjs';
import { AuthService } from './auth/auth.service';
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

  get isAuthenticated$ () {
    return this.authService.isAuthenticated$;
  }
  
  constructor (
    private orderService: OrderService,
    private authService: AuthService,
    private router: Router,
  ) {}

  logOut () {
    this.authService.logOut();
    this.router.navigateByUrl('/auth');
  }
}
