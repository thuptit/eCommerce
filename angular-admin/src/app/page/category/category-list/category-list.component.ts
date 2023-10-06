import { Component, ViewChild } from '@angular/core';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CategoryGridParam, CategoryModel } from 'src/core/models/category.model';
import { CategoryService } from 'src/core/services/category.service';

@Component({
  selector: 'app-category-list',
  templateUrl: './category-list.component.html',
  styleUrls: ['./category-list.component.scss']
})
export class CategoryListComponent {
  displayedColumns: string[] = ['id', 'name', 'description'];
  dataSource = new MatTableDataSource<CategoryModel>();
  totalCount: number = 0;
  searchText: string = '';
  @ViewChild(MatPaginator) paginator = {} as MatPaginator;
  gridParam = { pageIndex: 0, pageSize: 10, searchText: '' } as CategoryGridParam;
  constructor(private _categoryService: CategoryService) {
  }

  ngOnInit() {
    this.getAllPaging();
  }

  getAllPaging() {
    this._categoryService.getAllPaging(this.gridParam).subscribe(response => {
      if (!response.Success) return;
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
}
