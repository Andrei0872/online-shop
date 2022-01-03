import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { catchError, Observable, tap } from 'rxjs';
import { AuthService } from 'src/app/auth/auth.service';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor (private authService: AuthService, private router: Router) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const newReq = request.clone({
      headers: new HttpHeaders({
        Authorization: `Bearer ${this.authService.userToken}`
      })
    });

    return next.handle(newReq).pipe(
      tap({ error: err => {
        if ([401, 403].includes(err.status)) {
          this.authService.logOut();
          this.router.navigateByUrl('/auth')
        }
      }})
    );
  }
}
