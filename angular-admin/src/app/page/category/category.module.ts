import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CategoryRoutingModule } from './category-routing.module';
import { CategoryComponent } from './category.component';
import { CategoryListComponent } from './category-list/category-list.component';
import { SharedModule } from 'src/shared/shared.module';
import { CreateOrUpdateCategoryComponent } from './create-or-update-category/create-or-update-category.component';


@NgModule({
  declarations: [
    CategoryComponent,
    CategoryListComponent,
    CreateOrUpdateCategoryComponent
  ],
  imports: [
    CommonModule,
    CategoryRoutingModule,
    SharedModule
  ]
})
export class CategoryModule { }
