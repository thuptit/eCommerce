import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { LoggerService } from './logger.service';
import { TokenAuthService } from './token-auth.service';
import { SendMessageChatModel } from '../models/chatting.model';
import { BehaviorSubject, Subject } from 'rxjs';
import { MessageCall } from 'src/app/page/chatting/realtime-call/types/message-call';

@Injectable({
  providedIn: 'root'
})
export class SignalrService extends BaseService {
  protected override GetUrlService(): string {
    return '';
  }
  public sentMessageEvent$ = new BehaviorSubject<number>(0);
  public receivedMessageEvent$ = new BehaviorSubject<SendMessageChatModel>({} as SendMessageChatModel);
  public receiveCall$ = new BehaviorSubject<MessageCall>({} as MessageCall);
  private chatConnection!: HubConnection;
  public startConnection = async () => {
    this.chatConnection = new HubConnectionBuilder()
      .withUrl(this.baseUrl + '/signalr-notification', { accessTokenFactory: () => this._authToken.getToken() })
      .withAutomaticReconnect()
      .build();
    this.chatConnection.keepAliveIntervalInMilliseconds = 500;
    await this.chatConnection.start()
      .then((data: any) => console.log('Connection Started ...'))
      .catch((error: any) => console.log(error));
  }
  public listenerMessage = () => {
    console.log("Start listening ...")
    this.chatConnection.on('ReceivedMessage', (data: SendMessageChatModel) => {
      this.receivedMessageEvent$.next(data);
    })
  }
  public sendMessage = async (data: SendMessageChatModel) => {
    await this.chatConnection.invoke('SendMessage', data)
      .then((data: any) => {
        this.sentMessageEvent$.next(data);
      });
  }
  public sendMessageCall = async (msg: MessageCall, receiverId: number) => {
    await this.chatConnection.invoke('SendCall', msg, receiverId);
  }
  public listenerCall = () => {
    console.log("Start Calling ...")
    this.chatConnection.on('Calling', (msg: MessageCall) => {
      console.log("socket listen", msg);
      this.receiveCall$.next(msg);
    })
  }
  constructor(private _httpClient: HttpClient, private _logger: LoggerService, private _authToken: TokenAuthService) {
    super(_httpClient);
  }
}
