import { Component, OnDestroy } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserAuth } from 'src/core/models/user-auth.model';
import { AuthenticationService } from 'src/core/services/authentication.service';
import { TokenAuthService } from 'src/core/services/token-auth.service';
import {
  SocialAuthService,
  SocialUser,
} from '@abacritt/angularx-social-login';
import { ResponseApi } from 'src/core/models/response.model';
import { AuthenticateModel } from 'src/core/models/authenticate.model';
import { flatMap, of } from 'rxjs';
import { UnsubscriberServiceService } from 'src/core/services/unsubscriber-service.service';
@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  socialUser!: SocialUser;
  isLoggedin?: boolean;
  loginForm: FormGroup = new FormGroup({
    username: new FormControl('', { validators: Validators.required }),
    password: new FormControl('', { validators: Validators.required })
  });

  constructor(private _authenticationService: AuthenticationService,
    private _router: Router,
    private _tokenAuth: TokenAuthService,
    private socialAuthService: SocialAuthService,
    private _unsubscribeService: UnsubscriberServiceService
  ) {
  }

  ngOnInit() {
    this.loginGoogle();
  }

  public login() {
    if (!this.loginForm.valid) {
      return;
    }
    this._authenticationService.login(
      this.loginForm.controls['username'].value,
      this.loginForm.controls['password'].value
    )
      .pipe(this._unsubscribeService.takeUntilDestroy)
      .subscribe((response) => {
        if (!response.Success) {
          return;
        }
        this.handleLoginSuccess(response);
      })
  }
  public loginGoogle() {
    this.socialAuthService.authState
      .pipe(
        flatMap((user) => {
          this.socialUser = user;
          this.isLoggedin = user != null;
          if (!user)
            return of({ Success: false, StatusCode: 500, ErrorMessages: 'Login failed' } as ResponseApi<any>);
          return this._authenticationService.loginWithGG(user.idToken);
        }),
        this._unsubscribeService.takeUntilDestroy
      )
      .subscribe((response) => {
        if (!response.Success) {
          return;
        }
        this.handleLoginSuccess(response);
      });
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.loginForm.controls[controlName].hasError(errorName);
  }
  public handleLoginSuccess = (response: ResponseApi<AuthenticateModel>) => {
    const userAuth = {
      userId: response.Result?.userId,
      email: response.Result?.email,
      roles: response.Result?.roles,
      userName: response.Result?.userName
    } as UserAuth;

    this._tokenAuth.setUser(userAuth);
    this._tokenAuth.setToken(response.Result?.accessToken);

    this._router.navigate(['/'])
  }
}
