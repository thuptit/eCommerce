import { Component, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { UserDataDialog, UserGridParam, UserModel } from 'src/core/models/user.model';
import { UserService } from 'src/core/services/user.service';
import { ComponentBase } from 'src/shared/component-base.component';
import { CreateOrUpdateUserComponent } from '../create-or-update-user/create-or-update-user.component';
import { DialogMode } from 'src/shared/constants';
import { DialogResultModel } from 'src/core/models/dialog-result.model';

@Component({
  selector: 'app-list-user',
  templateUrl: './list-user.component.html',
  styleUrls: ['./list-user.component.scss']
})
export class ListUserComponent extends ComponentBase{
  displayedColumns: string[] = ['id', 'username', 'phone','email', 'address', 'creationTime', 'action'];
  dataSource = new MatTableDataSource<UserModel>();
  totalCount: number = 0;
  searchText: string = '';
  isLoading: boolean = false;
  @ViewChild(MatPaginator) paginator = {} as MatPaginator;
  gridParam = { pageIndex: 0, pageSize: 10, searchText: '' } as UserGridParam;
  constructor(private _userService: UserService, public dialog: MatDialog){
    super();
  }
  ngOnInit(){
    this.getAllPaging();
  }
  onSearch() {
    this.gridParam.searchText = this.searchText;
    this.getAllPaging();
  }
  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }
  getAllPaging(){
    this._userService.getAllPaging(this.gridParam).subscribe(response => {
      this.isLoading = response.isLoading;
      if (!response.Success && response.isLoading) return;
      this.dataSource.data = (response.Result?.items ?? []);
      this.totalCount = response.Result?.totalCount ?? 0;
      setTimeout(() => {
        this.paginator.length = this.totalCount;
        this.paginator.pageIndex = this.gridParam.pageIndex;
      });
    })
  }
  onCreate(enterAnimationDuration: string, exitAnimationDuration: string) {
    const modalCreate = this.dialog.open(CreateOrUpdateUserComponent, {
      autoFocus: true,
      width: '500px',
      enterAnimationDuration,
      exitAnimationDuration,
      data: {
        title: `${this.GlobalString.CREATE} User`,
        model: {},
        mode: DialogMode.CREATE
      } as UserDataDialog
    });
    modalCreate.afterClosed().subscribe((result: DialogResultModel<string>) => {
      if (!result.isSuccess) return;
      this.getAllPaging();
    });
  }
  onChangePage(event: any){
    this.gridParam.pageIndex = event.pageIndex;
    this.gridParam.pageSize = event.pageSize;
    this.getAllPaging();
  }
  onDelete(id: number){

  }
}
