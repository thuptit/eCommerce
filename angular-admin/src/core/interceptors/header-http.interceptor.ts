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
    const isFormData = request.body instanceof FormData;
    let contentType = isFormData ? 'multipart/form-data' : 'application/json';
    const authReq = request.clone({
      headers: request.headers
        .set('Authorization', `Bearer ${token}`)
        .set('Accept', isFormData ? "multipart/form-data" : 'application/json, text/plain, */*')
    });

    return next.handle(authReq);
  }
}
