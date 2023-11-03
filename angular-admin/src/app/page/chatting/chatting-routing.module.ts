import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListChatComponent } from './list-chat/list-chat.component';

const routes: Routes = [
  {
    path: '', children: [
      {
        path: '', pathMatch: 'full', component: ListChatComponent
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ChattingRoutingModule { }
