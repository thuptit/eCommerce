import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { PageComponent } from './page.component';

const routes: Routes = [
  {
    path: '', component: PageComponent, children: [
      {
        path: '', redirectTo: 'home', pathMatch: 'full'
      },
      {
        path: 'home', component: HomeComponent
      },
      {
        path: 'category', loadChildren: () => import('./category/category.module').then(m => m.CategoryModule)
      },
      {
        path: 'chat', loadChildren: () => import('./chatting/chatting.module').then(m => m.ChattingModule)
      },
      {
        path: 'user', loadChildren: () => import('./user/user.module').then(m => m.UserModule)
      }
    ]
  },
  // {
  //   path: 'home', component: HomeComponent
  // },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PageRoutingModule { }
