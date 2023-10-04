import { NgModule } from "@angular/core";
import { MatFormFieldModule } from '@angular/material/form-field';
import { ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { AuthGuard } from "src/core/guards/auth.guard";
import { CookieService } from 'ngx-cookie-service';
import { HTTP_INTERCEPTORS, HttpClientModule } from "@angular/common/http";
import { ToastrModule } from "ngx-toastr";
import { HeaderHttpInterceptor } from "src/core/interceptors/header-http.interceptor";
import { TransformResponseInterceptor } from "src/core/interceptors/transform-response.interceptor";
import { MenuBarComponent } from './components/menu-bar/menu-bar.component';
import { NbEvaIconsModule } from '@nebular/eva-icons';
import { NbColumnsService, NbLayoutModule, NbMenuModule, NbSidebarModule, NbThemeModule, NbThemeService } from '@nebular/theme';

@NgModule({
    declarations: [
        MenuBarComponent
    ],
    imports: [
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
        MatButtonModule,
        HttpClientModule,
        ToastrModule.forRoot(),
        NbMenuModule,
        NbSidebarModule,
        NbThemeModule,
        NbLayoutModule,
        NbEvaIconsModule
    ],
    exports: [
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
        MatButtonModule,
        HttpClientModule,
        ToastrModule,
        MenuBarComponent,
        NbMenuModule,
        NbSidebarModule,
        NbThemeModule,
        NbLayoutModule,
        NbEvaIconsModule
    ],
    providers: [
        AuthGuard,
        CookieService,
        {
            provide: HTTP_INTERCEPTORS, useClass: HeaderHttpInterceptor, multi: true
        },
        {
            provide: HTTP_INTERCEPTORS, useClass: TransformResponseInterceptor, multi: true
        }
    ]
})

export class SharedModule { }