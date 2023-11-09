import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { LoggerService } from './logger.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService extends BaseService {
  protected override GetUrlService(): string {
    return '';
  }

  private notificationConnection!: HubConnection;
  public startConnection = () => {
    this.notificationConnection = new HubConnectionBuilder()
    .withUrl(this.baseUrl + '/signalr-notification')
    .build();
    this.notificationConnection.start()
    .then((data) => console.log('Connection Started ...', data))
    .then(() => this.getConnectionId())
    .catch((error)=> console.log(error));
  }
  public listenerNotification = () => {
    this.notificationConnection.on('Notify', (data)=> {
      this._logger.success(data);
    })
  }
  connectionId: any;
  private getConnectionId = () => {
    this.notificationConnection.invoke('getconnectionid')
    .then((data) => {
      console.log(data);
      this.connectionId = data;
    });
  }
  constructor(private _httpClient: HttpClient, private _logger: LoggerService) {
    super(_httpClient);
  }
}
