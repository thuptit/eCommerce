import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { LoggerService } from './logger.service';
import { TokenAuthService } from './token-auth.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService extends BaseService {
  protected override GetUrlService(): string {
    return '';
  }

  private chatConnection!: HubConnection;
  public startConnection = () => {
    this.chatConnection = new HubConnectionBuilder()
      .withUrl(this.baseUrl + '/signalr-notification')
      .build();
    this.chatConnection.start()
      .then((data: any) => console.log('Connection Started ...', data))
      .then(() => this.getConnectionId())
      .catch((error: any) => console.log(error));
  }
  public listenerNotification = () => {
    this.chatConnection.on('Notify', (data: any) => {
      this._logger.success(data);
    })
  }
  connectionId: any;
  private getConnectionId = () => {
    this.chatConnection.invoke('getconnectionid', this._authToken.getUser().userId)
      .then((data: any) => {
        console.log(data);
        this.connectionId = data;
      });
  }
  constructor(private _httpClient: HttpClient, private _logger: LoggerService, private _authToken: TokenAuthService) {
    super(_httpClient);
  }
}
