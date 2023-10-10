import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { UserAuth } from '../models/user-auth.model';

@Injectable({
  providedIn: 'root'
})
export class TokenAuthService {

  constructor(private _cookies: CookieService) { }

  setUser(userAuth: UserAuth) {
    this._cookies.set('userInfo', JSON.stringify(userAuth));
  }
  setToken(token: string | undefined) {
    if (token === undefined)
      return;
    this._cookies.set('token', token);
  }
  getUser(): UserAuth {
    return JSON.parse(this._cookies.get('userInfo'));
  }
  getToken(): string {
    return this._cookies.get('token');
  }
  removeAuthToken() {
    this._cookies.delete('token', '/');
    this._cookies.delete('userInfo', '/');
  }
}
