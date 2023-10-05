import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PagingModel, ResponseApi } from '../models/response.model';
import { CategoryGridParam, CategoryModel } from '../models/category.model';

@Injectable({
  providedIn: 'root'
})
export class CategoryService extends BaseService {
  protected override GetUrlService(): string {
    return "Category";
  }

  constructor(private httpClient: HttpClient) { super(httpClient) }

  getAllPaging(gridParam: CategoryGridParam): Observable<ResponseApi<PagingModel<CategoryModel>>> {
    return this.httpClient.post<any>(this.rootUrl + "/GetAllPaging", gridParam);
  }
}
