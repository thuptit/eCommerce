import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserDataDialog } from 'src/core/models/user.model';
import { CategoryService } from 'src/core/services/category.service';
import { ComponentBase } from 'src/shared/component-base.component';

@Component({
  selector: 'app-create-or-update-user',
  templateUrl: './create-or-update-user.component.html',
  styleUrls: ['./create-or-update-user.component.scss']
})
export class CreateOrUpdateUserComponent extends ComponentBase {
  formGroup!: FormGroup;
  isSaving: boolean = false;

  constructor(public dialogRef: MatDialogRef<CreateOrUpdateUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserDataDialog,
    private _categorySevice: CategoryService) {
    super();
  }

  ngOnInit(){
    this.initForm();
  }

  private initForm(){
    this.formGroup = new FormGroup({
      userName: new FormControl(this.data.model.userName, [Validators.required]),
      phoneNumber: new FormControl(this.data.model.phoneNumber, [Validators.required]),
      email: new FormControl(this.data.model.email, Validators.required),
      address: new FormControl(this.data.model.avatarUrl)
    });
  }

  onSubmit(){
    
  }
}
