import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { CreateUserModel, IdAndNameModel, UserGridParam, UserModel } from '../models/user.model';
import { Observable, catchError, of, startWith } from 'rxjs';
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

  getAllPaging(gridParam: UserGridParam): Observable<ResponseApi<PagingModel<UserModel>>> {
    return this.httpClient.post<any>(this.rootUrl + '/GetAllPaging', gridParam);
  }

  create(user: CreateUserModel): Observable<ResponseApi<string>> {
    const headers = new HttpHeaders();
    headers.set('Accept', "multipart/form-data");
    const formData = new FormData();
    formData.append('UserName', user.userName);
    formData.append('PhoneNumber', user.phoneNumber);
    formData.append('Email', user.email);
    formData.append('Address', user.address);
    formData.append('AvatarFile', user.avatarFile);
    return this.httpClient.post<any>(this.rootUrl + '/CreateUser', formData, { headers })
      .pipe(startWith({ isLoading: true, Success: false } as ResponseApi<string>),
        catchError((error => of({ isLoading: false, Success: false } as ResponseApi<string>))));
  }

  getUsers(): Observable<ResponseApi<IdAndNameModel[]>> {
    return this.httpClient.get<any>(this.rootUrl + '/GetAllUser');
  }
}
