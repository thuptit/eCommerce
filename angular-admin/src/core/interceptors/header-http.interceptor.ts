import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { TokenAuthService } from '../services/token-auth.service';

@Injectable()
export class HeaderHttpInterceptor implements HttpInterceptor {

  constructor(private _tokenAuth: TokenAuthService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token = this._tokenAuth.getToken();
    if (token === null || token === undefined || token === '') {
      return next.handle(request);
    }
    const authReq = request.clone({
      headers: request.headers.set('Content-type', 'application/json')
        .set('Authorization', `Bearer ${token}`)
    });
    return next.handle(authReq);
  }
}
