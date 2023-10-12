import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CategoryDataDialog, CategoryGridParam, CategoryModel } from 'src/core/models/category.model';
import { CategoryService } from 'src/core/services/category.service';
import { CreateOrUpdateCategoryComponent } from '../create-or-update-category/create-or-update-category.component';
import { ComponentBase } from 'src/shared/component-base.component';
import { DialogResultModel } from 'src/core/models/dialog-result.model';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent extends ComponentBase {
  displayedColumns: string[] = ['id', 'name', 'description', 'action'];
  dataSource = new MatTableDataSource<CategoryModel>();
  totalCount: number = 0;
  searchText: string = '';
  isLoading: boolean = false;
  @ViewChild(MatPaginator) paginator = {} as MatPaginator;
  gridParam = { pageIndex: 0, pageSize: 10, searchText: '' } as CategoryGridParam;
  constructor(private _categoryService: CategoryService, public dialog: MatDialog) {
    super();
  }

  ngOnInit() {
    this.getAllPaging();
  }

  getAllPaging() {
    this._categoryService.getAllPaging(this.gridParam).subscribe(response => {
      this.isLoading = response.isLoading;
      if (!response.Success && response.isLoading) return;
      this.dataSource.data = (response.Result?.items ?? []);
      this.totalCount = response.Result?.totalCount ?? 0;
      setTimeout(() => {
        this.paginator.length = this.totalCount;
        this.paginator.pageIndex = this.gridParam.pageIndex;
      });
    });
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  onChangePage(event: PageEvent) {
    this.gridParam.pageIndex = event.pageIndex;
    this.gridParam.pageSize = event.pageSize;
    this.getAllPaging();
  }
  onSearch() {
    this.gridParam.searchText = this.searchText;
    this.getAllPaging();
  }
  onCreate(enterAnimationDuration: string, exitAnimationDuration: string) {
    const modalCreate = this.dialog.open(CreateOrUpdateCategoryComponent, {
      autoFocus: true,
      width: '500px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: {
        title: `${this.GlobalString.CREATE} Category`,
        model: { name: '', description: '' }
      } as CategoryDataDialog
    });
    modalCreate.afterClosed().subscribe((result: DialogResultModel<string>) => {
      if (!result.isSuccess) return;
      this.getAllPaging();
    });
  }
}
