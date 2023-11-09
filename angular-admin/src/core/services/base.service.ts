import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export abstract class BaseService {
  private apiUrl = environment.apiUrl;
  constructor(_httpClient: HttpClient) { }
  protected get rootUrl(): string {
    return this.apiUrl + '/api/' + this.GetUrlService();
  }
  protected get baseUrl() {
    return this.apiUrl;
  }
  protected abstract GetUrlService(): string
}
