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

@NgModule({
    imports: [
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
        MatButtonModule,
        HttpClientModule,
        ToastrModule.forRoot(),
    ],
    exports: [
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
        MatButtonModule,
        HttpClientModule,
        ToastrModule
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