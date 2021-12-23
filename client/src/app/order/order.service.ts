import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, EMPTY, filter, Observable, Subject, tap } from 'rxjs';
import { Environment, ENV_CONFIG } from '../tokens';
import { Order } from './order.model';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  private URL: string;
  private orders = new BehaviorSubject<Order[] | null>(null);
  private errors = new Subject<string>();

  orders$ = this.orders.asObservable().pipe(
    tap(orders => orders === null && this.getAll()),
    filter(Boolean)
  );

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
}
