import { Component, Inject } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CategoryDataDialog, CategoryModel } from 'src/core/models/category.model';
import { DialogResultModel } from 'src/core/models/dialog-result.model';
import { CategoryService } from 'src/core/services/category.service';
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
  isSaving: boolean = false;
  constructor(public dialogRef: MatDialogRef<CreateOrUpdateCategoryComponent>,
    @Inject(MAT_DIALOG_DATA) public data: CategoryDataDialog,
    private _categorySevice: CategoryService
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
  onSubmit() {
    if (!this.formGroup.valid)
      return;
    const model = {
      id: this.categoryDataDialog.model.id,
      description: this.formGroup.controls['description'].value,
      name: this.formGroup.controls['name'].value
    } as CategoryModel;
    this._categorySevice.create(model).subscribe(response => {
      this.isSaving = response.isLoading;
      if (response.isLoading || !response.Success)
        return;
      this.dialogRef.close({ isSuccess: true, data: response.Result } as DialogResultModel<string>);
    });
  }
}
