import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { UserDataDialog } from 'src/core/models/user.model';
import { BootstrapperService } from 'src/core/services/bootstrapper.service';
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
  avatar!: ArrayBuffer | string;
  constructor(public dialogRef: MatDialogRef<CreateOrUpdateUserComponent>,
    @Inject(MAT_DIALOG_DATA) public data: UserDataDialog,
    private _categorySevice: CategoryService) {
    super();
  }

  ngOnInit() {
    this.setAvatar();
    this.initForm();
  }
  private setAvatar() {
    console.log(BootstrapperService.defaultAvatar)
    if (this.data.model.avatarUrl) {
      this.avatar = this.data.model.avatarUrl;
    }
    else {
      this.avatar = BootstrapperService.defaultAvatar;
    }
  }
  private initForm() {
    this.formGroup = new FormGroup({
      userName: new FormControl(this.data.model.userName, [Validators.required]),
      phoneNumber: new FormControl(this.data.model.phoneNumber, [Validators.required]),
      email: new FormControl(this.data.model.email, Validators.required),
      address: new FormControl(this.data.model.address),
    });
  }

  onSelectFile(event: any) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();

      reader.readAsDataURL(event.target.files[0]); // read file as data url

      reader.onload = (event) => { // called once readAsDataURL is completed
        this.avatar = event.target?.result ?? BootstrapperService.defaultAvatar;
      }
    }
  }
  onDeleteAvatar() {
  }
  onSubmit() {

  }
}
