import { APP_INITIALIZER, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from 'src/shared/shared.module';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NbMenuModule, NbSidebarModule, NbThemeModule, NbThemeService } from '@nebular/theme';
import {
  SocialLoginModule,
  SocialAuthServiceConfig,
  GoogleLoginProvider,
  GoogleSigninButtonDirective,
  GoogleSigninButtonModule,
} from '@abacritt/angularx-social-login';
import { environment } from 'src/environments/environment';
import { SweetAlert2LoaderService, SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';
import { BootstrapperService } from 'src/core/services/bootstrapper.service';

const clientId = environment.clientId;
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    SharedModule,
    BrowserAnimationsModule,
    NgbModule,
    NbThemeModule.forRoot({ name: 'light' }),
    NbSidebarModule.forRoot(),
    NbMenuModule.forRoot(),
    SocialLoginModule,
    GoogleSigninButtonModule,
    SweetAlert2Module.forRoot()
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(clientId),
          },
        ],
      } as SocialAuthServiceConfig,
    },
    SweetAlert2LoaderService,
    GoogleSigninButtonDirective
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
