import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { UserGridParam, UserModel } from '../models/user.model';
import { Observable } from 'rxjs';
import { PagingModel, ResponseApi } from '../models/response.model';

@Injectable({
  providedIn: 'root'
})
export class UserService extends BaseService {
  protected override GetUrlService(): string {
    return 'User';
  }

  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  getAllPaging(gridParam: UserGridParam): Observable<ResponseApi<PagingModel<UserModel>>>{
    return this.httpClient.post<any>(this.rootUrl + '/GetAllPaging',gridParam);
  }
}
