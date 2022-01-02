import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor (private authService: AuthService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const newReq = request.clone({
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.authService.userToken}`
      })
    });

    return next.handle(newReq);
  }
}
