import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { ResponseApi } from '../models/response.model';
import { ConfigurationModel } from '../models/configuration.model';

@Injectable({
  providedIn: 'root'
})
export class CommonService extends BaseService {
  protected override GetUrlService(): string {
    return 'Configuration';
  }
  public defaultAvatar!: string;
  constructor(private httpClient: HttpClient) {
    super(httpClient);
  }

  async loadConfiguration() {
    return await this.getConfiguration()
      .then(result => {
        this.defaultAvatar = result.Result?.defaultAvatar ?? '';
      })
  }

  getConfiguration(): Promise<ResponseApi<ConfigurationModel>> {
    return this.httpClient.get<any>(this.rootUrl).toPromise();
  }

}
