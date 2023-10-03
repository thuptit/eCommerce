import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ResponseApi } from '../models/response.model';
import { AuthenticateModel } from '../models/authenticate.model';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService extends BaseService {
  protected override GetUrlService(): string {
    return 'Authentication';
  }

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  login(userName: string, password: string): Observable<ResponseApi<AuthenticateModel>> {
    return this.httpClient.post<any>(this.rootUrl + '/LoginAdminSite', { userName, password })
  }
}
