import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PageRoutingModule } from './page-routing.module';
import { PageComponent } from './page.component';
import { HomeComponent } from './home/home.component';
import { SharedModule } from 'src/shared/shared.module';
import { NbThemeService, NbUserModule } from '@nebular/theme';


@NgModule({
  declarations: [
    PageComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    PageRoutingModule,
    SharedModule
  ],
  providers: []
})
export class PageModule { }
