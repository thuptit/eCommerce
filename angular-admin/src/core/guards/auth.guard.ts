import { Injectable } from "@angular/core";
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from "@angular/router";
import { Observable } from "rxjs";
import { Auth } from "src/shared/utils/auth.utils";
import { TokenAuthService } from "../services/token-auth.service";

@Injectable()
export class AuthGuard implements CanActivate {
  constructor(private _router: Router, private _tokenAuth: TokenAuthService) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    const token = this._tokenAuth.getToken();
    if (token === null || token === undefined || token === '') {
      this._router.navigate(['/login']);
      return false;
    }
    return true;
  }
}