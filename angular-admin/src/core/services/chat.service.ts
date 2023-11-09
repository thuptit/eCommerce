import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { HttpClient } from '@angular/common/http';
import { TokenAuthService } from './token-auth.service';
import { Observable } from 'rxjs';
import { ResponseApi } from '../models/response.model';
import { MessageChatModel, UserChatModel } from '../models/chatting.model';

@Injectable({
  providedIn: 'root'
})
export class ChatService extends BaseService {
  protected override GetUrlService(): string {
    return 'chatting';
  }

  constructor(private httpClient: HttpClient, private tokenAuth: TokenAuthService) {
    super(httpClient)
  }

  getListUserChat(): Observable<ResponseApi<UserChatModel[]>> {
    return this.httpClient.get<any>(this.rootUrl + '/GetListUserChat/' + this.tokenAuth.getUser().userId);
  }
  checkCreatedChat(friendId: number): Observable<ResponseApi<number>> {
    return this.httpClient.get<any>(this.rootUrl + '/GetConversation/' + friendId);
  }
  getListMessageChat(personalChatId: number): Observable<ResponseApi<MessageChatModel[]>> {
    return this.httpClient.get<any>(this.rootUrl + '/GetListMessageChat/' + personalChatId);
  }
}
