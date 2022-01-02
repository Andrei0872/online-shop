import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { Environment, ENV_CONFIG } from '../tokens';
import { LoginBody, RegisterBody } from './auth.model';

const USER_KEY = 'user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private URL;
  private authState = new BehaviorSubject(JSON.parse(localStorage.getItem(USER_KEY) ?? 'null'));

  get currentUser () {
    return this.authState.value;
  }

  get currentUserExists () {
    return !!this.currentUser && !!this.currentUser?.token;
  }

  constructor(
    @Inject(ENV_CONFIG) env: Environment,
    private httpClient: HttpClient,
  ) {
    this.URL = `${env.API_URL}/api/user`;
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
}
