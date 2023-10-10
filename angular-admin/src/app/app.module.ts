import { NgModule } from '@angular/core';
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
    GoogleSigninButtonModule
  ],
  providers: [
    {
      provide: 'SocialAuthServiceConfig',
      useValue: {
        autoLogin: true,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider(clientId),
          },
        ],
      } as SocialAuthServiceConfig,
    },
    GoogleSigninButtonDirective
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
