import { NgModule } from "@angular/core";
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
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
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatTableModule } from '@angular/material/table';
import { MatCardModule } from '@angular/material/card';
import { MatDialogModule } from '@angular/material/dialog';
import { NbActionsModule, NbContextMenuModule, NbIconModule, NbInputModule, NbLayoutModule, NbMenuModule, NbSelectModule, NbSidebarModule, NbThemeModule, NbThemeService, NbUserModule } from '@nebular/theme';
import { AppHeaderComponent } from './components/app-header/app-header.component';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { SweetAlert2LoaderService, SweetAlert2Module } from '@sweetalert2/ngx-sweetalert2';

@NgModule({
    declarations: [
        MenuBarComponent,
        AppHeaderComponent
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
        NbEvaIconsModule,
        MatPaginatorModule,
        MatTableModule,
        MatCardModule,
        NbIconModule,
        NbInputModule,
        NbContextMenuModule,
        NbActionsModule,
        NbSelectModule,
        MatDialogModule,
        MatProgressSpinnerModule,
        FormsModule,
        NbUserModule,
        SweetAlert2Module
    ],
    exports: [
        MatFormFieldModule,
        ReactiveFormsModule,
        MatInputModule,
        MatButtonModule,
        HttpClientModule,
        ToastrModule,
        MenuBarComponent,
        AppHeaderComponent,
        NbMenuModule,
        NbSidebarModule,
        NbThemeModule,
        NbLayoutModule,
        NbEvaIconsModule,
        MatPaginatorModule,
        MatTableModule,
        MatCardModule,
        NbIconModule,
        NbInputModule,
        NbContextMenuModule,
        NbActionsModule,
        NbSelectModule,
        FormsModule,
        MatDialogModule,
        MatProgressSpinnerModule,
        NbUserModule,
        SweetAlert2Module
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