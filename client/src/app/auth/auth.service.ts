import { HttpClient } from '@angular/common/http';
import { Inject, Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { Environment, ENV_CONFIG } from '../tokens';
import { LoginBody, RegisterBody } from './auth.model';

const TOKEN_KEY = 'token';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private URL;
  private authState = new BehaviorSubject(localStorage.getItem(TOKEN_KEY));

  constructor(
    @Inject(ENV_CONFIG) env: Environment,
    private httpClient: HttpClient,
  ) {
    this.URL = `${env.API_URL}/api/user`;
  }

  private saveToken (token: string) {
    this.authState.next(token);
    localStorage.setItem(TOKEN_KEY, token);
  }

  attemptLogin (body: LoginBody) {
    return this.httpClient.post(
      `${this.URL}/login`,
      body,
    ).pipe(
      tap(response => this.saveToken((response as any).data.token))
    );
  }

  attemptRegister (body: RegisterBody) {
    return this.httpClient.post(
      `${this.URL}/register`,
      body,
    ).pipe(
      tap(response => this.saveToken((response as any).data.token))
    );
  }
}
