import { Injectable } from '@angular/core';
import { SignalrService } from './signalr.service';
import { MessageCall } from 'src/app/page/chatting/realtime-call/types/message-call';
@Injectable({
  providedIn: 'root'
})
export class WebRtcService {
  constructor(private _signalR: SignalrService) { }

  sendMessage(msg: MessageCall) {
    this._signalR.sendMessageCall(msg);
  }
}
