import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, map } from 'rxjs';
import { LoggerService } from '../services/logger.service';
import { Router } from '@angular/router';

@Injectable()
export class TransformResponseInterceptor implements HttpInterceptor {

  constructor(private _logger: LoggerService, private _router: Router) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(map((event: HttpEvent<any>) => {
      return event;
    }), catchError((error: HttpErrorResponse) => {
      if (error.status === 500) {
        this._logger.error(error.error?.ErrorMessage);
      }
      if (error.status === 401) {
        this._logger.error(error.error?.ErrorMessage);
        this._router.navigate(['/login']);
      }
      if (error.status === 403) {
        this._logger.error(error.error?.ErrorMessage);
        this._router.navigate(['/']);
      }
      throw (error);
    }));
  }

  private handleError() {

  }
}
