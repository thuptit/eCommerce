import { AfterViewChecked, AfterViewInit, Component, ElementRef, EventEmitter, Output, QueryList, ViewChild, ViewChildren } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentBase } from 'src/shared/component-base.component';
import { AddConversationDialogComponent } from './add-conversation-dialog/add-conversation-dialog.component';
import { CreateConversationDataDialogModel, UserChatModel } from 'src/core/models/chatting.model';
import { DialogMode } from 'src/shared/constants';
import { ChatService } from 'src/core/services/chat.service';
import { UserService } from 'src/core/services/user.service';
import { interval, mergeMap, of } from 'rxjs';
import { ResponseApi } from 'src/core/models/response.model';
import { UserModel } from 'src/core/models/user.model';
import { NbUserComponent } from '@nebular/theme';

@Component({
  selector: 'app-list-user-chatting',
  templateUrl: './list-user-chatting.component.html',
  styleUrls: ['./list-user-chatting.component.scss']
})
export class ListUserChattingComponent extends ComponentBase {
  users: UserChatModel[] = [];
  @Output() hadConversation = new EventEmitter<any>();
  personalChatId: number = 0;
  onlineStatus$ = interval(10000);
  constructor(public dialog: MatDialog, private _chatService: ChatService,
    private _userService: UserService) {
    super();
  }

  ngOnInit() {
    this.getChatUsers();
    this.onlineStatus$.subscribe(() => {
      this.getOnlineUser();
    })
  }
  getOnlineUser() {
    this._chatService.getOnlineUser(this.users.map(item => item.friendId))
      .subscribe(response => {
        if (!response.Success)
          return;
        const result = response.Result ?? [];
        for (let i = 0; i < this.users.length; i++) {
          this.users[i].isOnline = result[i];
        }
      })
  }
  getChatUsers() {
    this._chatService.getListUserChat()
      .subscribe(response => {
        if (!response.Success) return;
        this.users = response.Result ?? [];
      })
  }
  openWindowChat(user: UserChatModel) {
    this.handleChatAction(user.friendId);
  }
  openVideoCall(user: UserChatModel) {
    this.hadConversation.emit({ friendId: user.friendId });
  }
  openAddConversationDialog() {
    const dialogRef = this.dialog.open(AddConversationDialogComponent, {
      autoFocus: true,
      width: '500px',
      data: { title: 'Create new Conversation', mode: DialogMode.CREATE } as CreateConversationDataDialogModel
    });
    dialogRef.afterClosed().subscribe(friendId => {
      this.handleChatAction(friendId);
    });
  }

  handleChatAction(friendId: number) {
    this._chatService.checkCreatedChat(friendId)
      .pipe(mergeMap(data => {
        if (!data.Success)
          return of({} as ResponseApi<UserModel>);
        this.personalChatId = data.Result ?? 0;
        return this._userService.getUserInfo(friendId);
      }))
      .subscribe(response => {
        if (!response.Success) return;
        this.hadConversation.emit({ personalChatId: this.personalChatId, friendInfo: response.Result });
        if (this.users.findIndex(x => x.personalChatId === this.personalChatId) >= 0)
          return;
        this.users.push({ friendId: response.Result?.id, friendName: response.Result?.userName } as UserChatModel);
      });
  }
}
