import { Component, EventEmitter, Output } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentBase } from 'src/shared/component-base.component';
import { AddConversationDialogComponent } from './add-conversation-dialog/add-conversation-dialog.component';
import { CreateConversationDataDialogModel, UserChatModel } from 'src/core/models/chatting.model';
import { DialogMode } from 'src/shared/constants';
import { ChatService } from 'src/core/services/chat.service';

@Component({
  selector: 'app-list-user-chatting',
  templateUrl: './list-user-chatting.component.html',
  styleUrls: ['./list-user-chatting.component.scss']
})
export class ListUserChattingComponent extends ComponentBase {
  users: UserChatModel[] = [];
  @Output() hadConversation = new EventEmitter<number>();
  constructor(public dialog: MatDialog, private _chatService: ChatService) {
    super();
  }
  openAddConversationDialog() {
    const dialogRef = this.dialog.open(AddConversationDialogComponent, {
      autoFocus: true,
      width: '500px',
      data: { title: 'Create new Conversation', mode: DialogMode.CREATE } as CreateConversationDataDialogModel
    });
    dialogRef.afterClosed().subscribe(friendId => {
      this._chatService.checkCreatedChat(friendId)
        .subscribe(response => {
          if (!response.Success) return;
          this.hadConversation.emit(response.Result);
        });
    });
  }
}
