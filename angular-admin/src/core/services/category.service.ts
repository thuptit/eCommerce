import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable, catchError, map, of, startWith } from 'rxjs';
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
    return this.httpClient.post<any>(this.rootUrl + "/GetAllPaging", gridParam)
      .pipe(
        startWith({ isLoading: true, Success: false } as ResponseApi<PagingModel<CategoryModel>>),
        catchError(error => {
          return of({ isLoading: false, Success: false } as ResponseApi<PagingModel<CategoryModel>>)
        })
      );
  }
  create(model: CategoryModel): Observable<ResponseApi<string>> {
    return this.httpClient.post<any>(this.rootUrl, model)
      .pipe(
        startWith({ isLoading: true, Success: false } as ResponseApi<string>),
        catchError(error => {
          return of({ isLoading: false, Success: false } as ResponseApi<string>)
        })
      );
  }
  delete(id: number): Observable<ResponseApi<string>> {
    return this.httpClient.delete<any>(this.rootUrl + `/${id}`);
  }
}
