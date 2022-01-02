import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, filter, Observable, tap, map, Subject, catchError, EMPTY } from 'rxjs';
import { Environment, ENV_CONFIG } from '../tokens';
import { Product } from './product.model';

@Injectable({
  providedIn: 'root'
})
export class ProductService {
  private URL: string;
  private products = new BehaviorSubject<Product[] | null>(null);
  private errors = new Subject<string>();

  products$ = this.products.asObservable()
    .pipe(
      tap(products => products === null && this.getAll()),
      filter(Boolean)
    );

  constructor (
    @Inject(ENV_CONFIG) env: Environment,
    private httpClient: HttpClient,
  ) {
    this.URL = `${env.API_URL}/product`;
  }

  private getAll () {
    const pendingProducts$: Observable<Product[]> = this.httpClient.get(this.URL) as Observable<Product[]>;
    pendingProducts$.pipe(
      map(resp => (resp as any).data),
      catchError(err => (console.warn(err), this.errors.next(err), EMPTY)),
    ).subscribe(p => this.products.next(p));
  }
}
