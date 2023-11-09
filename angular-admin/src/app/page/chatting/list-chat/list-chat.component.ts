import { Component } from '@angular/core';
import { ComponentBase } from 'src/shared/component-base.component';

@Component({
  selector: 'app-list-chat',
  templateUrl: './list-chat.component.html',
  styleUrls: ['./list-chat.component.scss']
})
export class ListChatComponent extends ComponentBase {
  isOpenConversation: boolean = false;
  constructor() {
    super();
  }
  openConversation(event: any) {
    console.log(event);

  }
}
