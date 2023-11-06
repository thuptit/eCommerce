import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { Observable, lastValueFrom, tap } from 'rxjs';
import { ResponseApi } from '../models/response.model';
import { ConfigurationModel } from '../models/configuration.model';

@Injectable({
  providedIn: 'root'
})
export class BootstrapperService extends BaseService {
  protected override GetUrlService(): string {
    return 'Configuration';
  }

  public static defaultAvatar: string;

  constructor(private _httpClient: HttpClient) {
    super(_httpClient);
  }
  loadConfigurationData(): Promise<any> {
    return this.getConfiguration()
      .pipe(tap(source => {
        BootstrapperService.defaultAvatar = source.Result?.defaultAvatar ?? '';
      }))
      .toPromise();
  }
  getConfiguration(): Observable<ResponseApi<ConfigurationModel>> {
    return this._httpClient.get<any>(this.rootUrl);
  }
}
