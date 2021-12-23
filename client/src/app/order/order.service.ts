import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, EMPTY, filter, map, mapTo, Observable, Subject, tap, timer } from 'rxjs';
import { Environment, ENV_CONFIG } from '../tokens';
import { CurrentOrder, Order } from './order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private URL: string;
  private orders = new BehaviorSubject<Order[] | null>(null);
  private errors = new Subject<string>();
  private currentOrder = new BehaviorSubject<CurrentOrder | null>(null);

  orders$ = this.orders.asObservable().pipe(
    tap(orders => orders === null && this.getAll()),
    filter(Boolean)
  );

  currentOrder$ = this.currentOrder.asObservable();

  constructor(
    @Inject(ENV_CONFIG) env: Environment,
    private httpClient: HttpClient
  ) {
    this.URL = `${env.API_URL}/orders`;
  }

  private getAll () {
    const pendingOrders$: Observable<Order[]> = this.httpClient.get(this.URL) as Observable<Order[]>;
    pendingOrders$.pipe(
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
}
