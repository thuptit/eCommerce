import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { LoggerService } from './logger.service';
import { TokenAuthService } from './token-auth.service';
import { SendMessageChatModel } from '../models/chatting.model';
import { BehaviorSubject, Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalrService extends BaseService {
  protected override GetUrlService(): string {
    return '';
  }
  public sentMessageEvent$ = new BehaviorSubject<number>(0);
  public receivedMessageEvent$ = new BehaviorSubject<SendMessageChatModel>({} as SendMessageChatModel);
  private chatConnection!: HubConnection;
  public startConnection = async () => {
    this.chatConnection = new HubConnectionBuilder()
      .withUrl(this.baseUrl + '/signalr-notification')
      .build();
    await this.chatConnection.start()
      .then((data: any) => console.log('Connection Started ...'))
      .then(() => this.getConnectionId())
      .catch((error: any) => console.log(error));
  }
  public listenerMessage = () => {
    console.log("Start listening ...")
    this.chatConnection.on('ReceivedMessage', (data: SendMessageChatModel) => {
      this.receivedMessageEvent$.next(data);
    })
  }
  public sendMessage = (data: SendMessageChatModel) => {
    this.chatConnection.invoke('SendMessage', data)
      .then((data: any) => {
        this.sentMessageEvent$.next(data);
      });
  }
  connectionId: any;
  private getConnectionId = () => {
    this.chatConnection.invoke('getconnectionid', this._authToken.getUser().userId)
      .then((data: any) => {
        this.connectionId = data;
      });
  }
  constructor(private _httpClient: HttpClient, private _logger: LoggerService, private _authToken: TokenAuthService) {
    super(_httpClient);
  }
}
