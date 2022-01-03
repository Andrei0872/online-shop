import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, catchError, EMPTY, filter, map, Observable, tap } from 'rxjs';
import { Environment, ENV_CONFIG } from '../tokens';
import { User } from './user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private URL: string;
  private users = new BehaviorSubject<User[] | null>(null);

  users$ = this.users.asObservable()
    .pipe(
      tap(users => users === null && this.getAll()),
      filter(Boolean)
    );

  constructor(
    @Inject(ENV_CONFIG) env: Environment,
    private httpClient: HttpClient,
  ) {
    this.URL = `${env.API_URL}/user`;
  }

  private getAll () {
    const pendingUsers$: Observable<User[]> = this.httpClient.get(this.URL) as Observable<User[]>;
    pendingUsers$.pipe(
      map(resp => (resp as any).data),
      catchError(err => (console.warn(err), EMPTY)),
    ).subscribe(p => this.users.next(p));
  }
}
