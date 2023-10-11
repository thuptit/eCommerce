import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoryDataDialog } from 'src/core/models/category.model';
import { ComponentBase } from 'src/shared/component-base.component';
import { DialogMode } from 'src/shared/constants';

@Component({
  selector: 'app-create-or-update-category',
  templateUrl: './create-or-update-category.component.html',
  styleUrls: ['./create-or-update-category.component.scss']
})
export class CreateOrUpdateCategoryComponent extends ComponentBase {
  mode!: DialogMode;
  categoryDataDialog!: CategoryDataDialog;
  formGroup!: FormGroup;
  constructor(public dialogRef: MatDialogRef<CreateOrUpdateCategoryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CategoryDataDialog
  ) {
    super();
    this.categoryDataDialog = this.data;
    this.mode = this.data.model.id ? DialogMode.CREATE : DialogMode.EDIT;
  }
  ngOnInit() {
    this.initForm();
  }
  private initForm() {
    this.formGroup = new FormGroup({
      name: new FormControl(this.categoryDataDialog.model.name, [Validators.required]),
      description: new FormControl(this.categoryDataDialog.model.description)
    });
  }
}
