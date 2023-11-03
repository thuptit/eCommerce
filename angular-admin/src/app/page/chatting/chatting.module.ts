import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { ChattingRoutingModule } from './chatting-routing.module';
import { ListChatComponent } from './list-chat/list-chat.component';
import { ChattingComponent } from './chatting.component';
import { SharedModule } from 'src/shared/shared.module';
import { ListUserChattingComponent } from './list-user-chatting/list-user-chatting.component';
import { ListMessageChattingComponent } from './list-message-chatting/list-message-chatting.component';
import { NbCardModule, NbChatModule, NbListModule } from '@nebular/theme';


@NgModule({
  declarations: [
    ListChatComponent,
    ChattingComponent,
    ListUserChattingComponent,
    ListMessageChattingComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    ChattingRoutingModule,
    NbListModule,
    NbCardModule,
    NbChatModule.forChild()
  ]
})
export class ChattingModule { }