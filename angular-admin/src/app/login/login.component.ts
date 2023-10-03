import { Component } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { UserAuth } from 'src/core/models/user-auth.model';
import { AuthenticationService } from 'src/core/services/authentication.service';
import { LoggerService } from 'src/core/services/logger.service';
import { TokenAuthService } from 'src/core/services/token-auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent {
  loginForm: FormGroup = new FormGroup({
    username: new FormControl('', { validators: Validators.required }),
    password: new FormControl('', { validators: Validators.required })
  });

  constructor(private _authenticationService: AuthenticationService,
    private _router: Router,
    private _tokenAuth: TokenAuthService,
    private _logger: LoggerService
  ) {
  }

  public login() {
    if (!this.loginForm.valid) {
      return;
    }
    this._authenticationService.login(
      this.loginForm.controls['username'].value,
      this.loginForm.controls['password'].value
    ).subscribe((response) => {
      if (!response.Success) {
        return;
      }

      const userAuth = {
        userId: response.Result?.userId,
        email: response.Result?.email,
        roles: response.Result?.roles,
        userName: response.Result?.userName
      } as UserAuth;

      this._tokenAuth.setUser(userAuth);
      this._tokenAuth.setToken(response.Result?.accessToken);

      this._router.navigate(['/'])
    })
  }
  public checkError = (controlName: string, errorName: string) => {
    return this.loginForm.controls[controlName].hasError(errorName);
  }
}
