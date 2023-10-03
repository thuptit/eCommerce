import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { PageComponent } from './page/page.component';
import { AuthGuard } from 'src/core/guards/auth.guard';

const routes: Routes = [
  {
    path: '', redirectTo: 'eCommerce', pathMatch: 'full'
  },
  {
    path: 'login', component: LoginComponent
  },
  {
    path: 'eCommerce', 
    loadChildren: () => import('./page/page.module').then(m => m.PageModule),
    canActivate: [AuthGuard]
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
