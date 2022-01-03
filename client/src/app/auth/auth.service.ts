import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, map, tap } from 'rxjs';
import { Environment, ENV_CONFIG } from '../tokens';
import { LoginBody, RegisterBody } from './auth.model';

const USER_KEY = 'user';

const enum USER_ROLES {
  ADMIN = "Admin",
  USER = "User"
};

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private URL;
  private authState = new BehaviorSubject(JSON.parse(localStorage.getItem(USER_KEY) ?? 'null'));

  authState$ = this.authState.asObservable();

  isAuthenticated$ = this.authState$.pipe(
    map(state => !!state)
  );

  get currentUser () {
    return this.authState.value;
  }

  get currentUserExists () {
    return !!this.currentUser && !!this.currentUser?.token;
  }

  get userToken () {
    return this.currentUser?.token;
  }

  get isCurrentUserAdmin () {
    return this.currentUser?.role === USER_ROLES.ADMIN;
  }

  constructor(
    @Inject(ENV_CONFIG) env: Environment,
    private httpClient: HttpClient,
  ) {
    this.URL = `${env.API_URL}/user`;
  }

  private saveAuthState ({ token, role }: any) {
    const newState = { token, role };

    this.authState.next(newState);
    localStorage.setItem(USER_KEY, JSON.stringify(newState));
  }

  attemptLogin (body: LoginBody) {
    return this.httpClient.post(
      `${this.URL}/login`,
      body,
    ).pipe(
      tap(response => this.saveAuthState((response as any).data))
    );
  }

  attemptRegister (body: RegisterBody) {
    return this.httpClient.post(
      `${this.URL}/register`,
      body,
    ).pipe(
      tap(response => this.saveAuthState((response as any).data))
    );
  }

  logOut () {
    this.authState.next(null);
    localStorage.removeItem(USER_KEY);
  }
}
