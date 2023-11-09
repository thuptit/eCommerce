import { Component, Inject } from '@angular/core';
import { FormControl } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Observable } from 'rxjs';
import { CreateConversationDataDialogModel } from 'src/core/models/chatting.model';
import { ComponentBase } from 'src/shared/component-base.component';

@Component({
  selector: 'app-add-conversation-dialog',
  templateUrl: './add-conversation-dialog.component.html',
  styleUrls: ['./add-conversation-dialog.component.scss']
})
export class AddConversationDialogComponent extends ComponentBase {
  userNameControl = new FormControl('');
  options: string[] = ['One', 'Two', 'Three'];
  filteredOptions!: Observable<string[]>;
  constructor(public dialogRef: MatDialogRef<AddConversationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CreateConversationDataDialogModel){
    super();
  }
}
