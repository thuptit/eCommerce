import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { SendMessageChatModel } from 'src/core/models/chatting.model';
import { UserModel } from 'src/core/models/user.model';
import { ChatService } from 'src/core/services/chat.service';
import { SignalrService } from 'src/core/services/signalr.service';
import { TokenAuthService } from 'src/core/services/token-auth.service';
import { UserService } from 'src/core/services/user.service';
import { ComponentBase } from 'src/shared/component-base.component';

@Component({
  selector: 'app-list-message-chatting',
  templateUrl: './list-message-chatting.component.html',
  styleUrls: ['./list-message-chatting.component.scss'],
})
export class ListMessageChattingComponent extends ComponentBase {
  @Input() openConversationId!: number;
  @Input() friendInfo!: UserModel;
  myInfo: any;
  messages: any[] = [];
  constructor(private _chatService: ChatService,
    private _signalR: SignalrService,
    private _userService: UserService,
    private _tokenAuth: TokenAuthService
  ) {
    super();
  }
  ngOnInit(): void {
    this.getMyInfo();
    this.loadMessages();
    setTimeout(() => {
      this.receivedMessageListener();
      this.sentMessageListener();
    });
  }

  ngOnChanges() {
    this.loadMessages();
  }

  private getMyInfo() {
    this.myInfo = this._tokenAuth.getUser();
  }

  sendMessage(event: any): void {
    const message = {
      text: event.message,
      date: new Date(),
      reply: true,
      type: 'text',
      user: {
        name: this.myInfo.userName,
        avatar: this.myInfo.avatarUrl,
      },
    }
    this.messages.push(message);
    this._signalR.sendMessage({
      message: message.text,
      personalChatId: this.openConversationId,
      senderId: this.myInfo.userId,
      receiverId: this.friendInfo.id
    } as SendMessageChatModel);
  }

  private loadMessages(): void {
    this._chatService.getListMessageChat(this.openConversationId)
      .subscribe(response => {
        if (!response.Success) return;
        const lst = response.Result?.map(item => {
          return {
            text: item.message,
            type: 'text',
            reply: item.senderId !== this.myInfo.userId ? false : true,
            date: item.seenDate,
            user: {
              name: item.senderName,
              avatar: item.avatarUrl
            }
          }
        }) as [];
        this.messages = [...lst];
      })
  }
  sentMessageListener() {
    this._signalR.sentMessageEvent$.asObservable().subscribe(data => {
      if (!data) return;
      this.openConversationId = data;
    })
  }
  receivedMessageListener() {
    this._signalR.receivedMessageEvent$.asObservable()
      .subscribe(data => {
        if (!data || data.personalChatId !== this.openConversationId)
          return;
        this.messages.push({
          text: data.message,
          type: 'text',
          reply: false,
          date: new Date(),
          user: {
            name: data.senderId === this.myInfo.id ? this.myInfo.userName : this.friendInfo.userName,
            avatar: data.senderId === this.myInfo.id ? this.myInfo.avatarUrl : this.friendInfo.avatarUrl
          }
        });
      });
  }
}
