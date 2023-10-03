import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService {
  protected apiUrl = environment.apiUrl;
  constructor(_httpClient: HttpClient) { }
  get rootUrl(): string {
    return this.apiUrl + this.GetUrlService();
  }
  protected abstract GetUrlService(): string
}
