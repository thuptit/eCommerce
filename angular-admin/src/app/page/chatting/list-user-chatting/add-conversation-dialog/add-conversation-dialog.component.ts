import { Component, Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CreateConversationDataDialogModel } from 'src/core/models/chatting.model';
import { IdAndNameModel } from 'src/core/models/user.model';
import { UserService } from 'src/core/services/user.service';
import { ComponentBase } from 'src/shared/component-base.component';

@Component({
  selector: 'app-add-conversation-dialog',
  templateUrl: './add-conversation-dialog.component.html',
  styleUrls: ['./add-conversation-dialog.component.scss']
})
export class AddConversationDialogComponent extends ComponentBase {
  selectedValue!: number;
  isSaving: boolean = false;
  users: IdAndNameModel[] = [];
  constructor(public dialogRef: MatDialogRef<AddConversationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CreateConversationDataDialogModel,
    private _userService: UserService) {
    super();
  }

  ngOnInit() {
    this.getUsers();
  }

  private getUsers() {
    this._userService.getUsers()
      .subscribe(response => {
        if (!response.Success) return;
        this.users = response.Result ?? [];
      })
  }

  onSave() {
    this.dialogRef.close(this.selectedValue);
  }
}
