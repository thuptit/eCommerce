import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { UserRoutingModule } from './user-routing.module';
import { UserComponent } from './user.component';
import { ListUserComponent } from './list-user/list-user.component';
import { SharedModule } from 'src/shared/shared.module';
import { CreateOrUpdateUserComponent } from './create-or-update-user/create-or-update-user.component';


@NgModule({
  declarations: [UserComponent, ListUserComponent, CreateOrUpdateUserComponent],
  imports: [
    CommonModule,
    UserRoutingModule,
    SharedModule
  ]
})
export class UserModule { }
