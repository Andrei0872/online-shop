import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, EMPTY, filter, map, mapTo, Observable, scan, Subject, tap, timer, withLatestFrom } from 'rxjs';
import { Product } from '../product/product.model';
import { Environment, ENV_CONFIG } from '../tokens';
import { CartEvent, CartPayload, CurrentOrder, Order, OrderProduct } from './order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private URL: string;
  private orders = new BehaviorSubject<Order[] | null>(null);
  private errors = new Subject<string>();
  private currentOrder = new BehaviorSubject<CurrentOrder | null>(null);
  private cart = new BehaviorSubject<OrderProduct[]>([]);
  private cartEvents = new Subject<CartPayload>();

  orders$ = this.orders.asObservable().pipe(
    tap(orders => orders === null && this.getAll()),
    filter(Boolean)
  );

  currentOrder$ = this.currentOrder.asObservable();

  cart$ = this.cart.asObservable();

  constructor(
    @Inject(ENV_CONFIG) env: Environment,
    private httpClient: HttpClient
  ) {
    this.URL = `${env.API_URL}/order`;

    this.cartEvents.pipe(
      withLatestFrom(this.cart),
      map(([cartPayload, products]) => {
        if (cartPayload.event === CartEvent.ADD) {
          const newProduct = cartPayload.product;
          const existingProductIdx = products.findIndex(p => +p.id === +newProduct.id);
          
          if (existingProductIdx === -1) {
            return [...products, newProduct];
          }

          products[existingProductIdx].quantity++;
          return [...products];
        }

        if (cartPayload.event === CartEvent.DELETE) {
          return products.filter(p => +p.id !== +cartPayload.productId);
        }

        return [];
      }),
    ).subscribe(this.cart);
  }

  private getAll () {
    const pendingOrders$: Observable<Order[]> = this.httpClient.get(this.URL) as Observable<Order[]>;
    pendingOrders$.pipe(
      map(r => (r as any).data),
      catchError(err => (console.warn(err), this.errors.next(err), EMPTY)),
    ).subscribe(p => this.orders.next(p));
  }

  fetchOrder (id: number) {
    const URL = `${this.URL}/${id}`;
    this.httpClient.get(URL)
      .pipe(
        map(r => (r as any).data),
      )
      .subscribe((order: CurrentOrder) => this.currentOrder.next(order))
  }

  addProductToCart (product: Product) {
    this.cartEvents.next({
      event: CartEvent.ADD,
      product: { ...product, quantity: 1 },
    });
  }

  submitOrder (products: OrderProduct[]) {
    const orderProducts = products.map(p => ({ id: p.id, quantity: p.quantity }));
    
    return this.httpClient.post(this.URL, { products: orderProducts })
      .pipe(
        map(resp => (resp as any).data),
        catchError(err => (console.warn(err), this.errors.next(err), EMPTY)),
      );
  }

  markOrdersAsDirty () {
    this.orders.next(null);
  }

  // Use case: as an admin, a user's order has been viewed,
  // so, `currentOrder` will **not** be null. Then, the admin updated
  // a product. There is a chance the updated product is part of the previously
  // viewed order's products, so we need to ensure that there is no stale data.
  markCurrentOrderAsDirty () {
    this.currentOrder.next(null);
  }

  emptyCurrentCart () {
    this.cart.next([]);
  }

  fetchOrdersOfUser (userId: number) {
    const URL = `${this.URL}/user/${userId}`;
    return this.httpClient.get(URL).pipe(
      map(resp => (resp as any).data),
      catchError(err => (console.warn(err), this.errors.next(err), EMPTY)),
    );
  }
}
