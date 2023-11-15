import { Component } from '@angular/core';
import { UserModel } from 'src/core/models/user.model';
import { ComponentBase } from 'src/shared/component-base.component';

@Component({
  selector: 'app-list-chat',
  templateUrl: './list-chat.component.html',
  styleUrls: ['./list-chat.component.scss']
})
export class ListChatComponent extends ComponentBase {
  personalChatId!: number;
  friendInfo!: UserModel;
  isShowWindowChat: boolean = false;
  isShowVideoCall: boolean = false;
  receiverId!: number;
  constructor() {
    super();
  }
  openConversation(event: any) {
    if (event.friendId) {
      this.isShowWindowChat = false;
      this.isShowVideoCall = true;
      this.receiverId = event.friendId;
    }
    else {
      this.personalChatId = event.personalChatId;
      this.friendInfo = event.friendInfo;
      this.isShowWindowChat = true;
      this.isShowVideoCall = false;
    }
  }
}
