import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ComponentBase } from 'src/shared/component-base.component';
import { AddConversationDialogComponent } from './add-conversation-dialog/add-conversation-dialog.component';
import { CreateConversationDataDialogModel } from 'src/core/models/chatting.model';
import { DialogMode } from 'src/shared/constants';

@Component({
  selector: 'app-list-user-chatting',
  templateUrl: './list-user-chatting.component.html',
  styleUrls: ['./list-user-chatting.component.scss']
})
export class ListUserChattingComponent extends ComponentBase {
  users: { name: string, title: string }[] = [
    { name: 'Carla Espinosa', title: 'Nurse' },
    { name: 'Bob Kelso', title: 'Doctor of Medicine' },
    { name: 'Janitor', title: 'Janitor' },
    { name: 'Perry Cox', title: 'Doctor of Medicine' },
    { name: 'Ben Sullivan', title: 'Carpenter and photographer' },
  ];
  constructor(public dialog: MatDialog){
    super();
  }
  openAddConversationDialog(){
    const dialogRef = this.dialog.open(AddConversationDialogComponent, {
      autoFocus: true,
      width: '500px',
      data: {title: 'Create new Conversation', mode: DialogMode.CREATE} as CreateConversationDataDialogModel
    });
    dialogRef.afterClosed().subscribe(result => {

    });
  }
}
