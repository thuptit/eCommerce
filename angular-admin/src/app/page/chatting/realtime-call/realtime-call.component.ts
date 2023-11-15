import { AfterViewInit, Component, ElementRef, Input, ViewChild } from '@angular/core';
import { environment } from 'src/environments/environment';
import { RtcEventHandler, RtcHandlerMessage } from './types/rtc-handler';
import { MessageCall } from './types/message-call';
import { SignalrService } from 'src/core/services/signalr.service';

const offerOptions = {
  offerAudio: true,
  offerVideo: true
} as RTCOfferOptions;

const mediaConstraints = {
  audio: true,
  video: { width: 1280, height: 720 }
};

@Component({
  selector: 'app-realtime-call',
  templateUrl: './realtime-call.component.html',
  styleUrls: ['./realtime-call.component.scss']
})
export class RealtimeCallComponent implements AfterViewInit, RtcHandlerMessage, RtcEventHandler {
  @Input() receiverId!: number;
  private peerConnection!: RTCPeerConnection;
  private localStream!: MediaStream;

  inCall: boolean = false;
  localVideoActive: boolean = false;

  @ViewChild('local_video') localVideo!: ElementRef;
  @ViewChild('received_video') remoteVideo!: ElementRef;

  constructor(private _signalR: SignalrService) {
  }
  async ngAfterViewInit() {
    await this.requestMediaDevice();
    this.addIncominMessageHandler();
  }

  private addIncominMessageHandler() {
    this._signalR.receiveCall$.asObservable().subscribe({
      next: (msg) => {
        console.log("incoming calling", msg);
        switch (msg.type) {
          case 'offer':
            this.handleOfferMessage(msg.data);
            break;
          case 'answer':
            this.handleAnswerMessage(msg.data);
            break;
          case 'hangup':
            this.handleHangupMessage(msg);
            break;
          case 'ice-candidate':
            this.handleICECandidateMessage(msg.data);
            break;
          default:
            console.log('unknown message of type ' + msg.type);
        }
      },
      complete: () => {
        console.log("Complete event")
      },
      error(err) {
        console.log("event error");
      },
    }
    );
  }
  private handleGetUserMediaError(e: Error) {
    switch (e.name) {
      case 'NotFoundError':
        alert('Unable to open your call because no camera and/or microphone were found.');
        break;
      case 'SecurityError':
      case 'PermissionDeniedError':
        // Do nothing; this is the same as the user canceling the call.
        break;
      default:
        console.log(e);
        alert('Error opening your camera and/or microphone: ' + e.message);
        break;
    }

    //close call
    this.closeCall();
  }
  // event
  handleICECandidateEvent = (event: RTCPeerConnectionIceEvent) => {
    console.log(event);
    if (event.candidate) {
      this._signalR.sendMessageCall({
        type: 'ice-candidate',
        data: event.candidate
      }, this.receiverId);
    }
  };
  handleICEConnectionStateChangeEvent = (event: Event) => {
    console.log(event);
    switch (this.peerConnection.iceConnectionState) {
      case 'closed':
      case 'failed':
      case 'disconnected':
        this.closeCall();
    }
  };
  handleSignalingStateChangeEvent = (event: Event) => {
    switch (this.peerConnection.signalingState) {
      case 'closed':
        this.closeCall();
        break;
    }
  };
  handleTrackEvent = (event: RTCTrackEvent) => {
    console.log("on track remote:", event);
    this.remoteVideo.nativeElement.srcObject = event.streams[0];
  };
  //end event

  //request media devices
  private async requestMediaDevice() {
    try {
      this.localStream = await navigator.mediaDevices.getUserMedia(mediaConstraints);

      this.pauseLocalVideo();
    }
    catch (e) {
      console.error(e);
    }
  }
  async hangUp() {
    await this._signalR.sendMessageCall({ type: 'hangup', data: '' }, this.receiverId);
    this.closeCall();
  }
  async startCallLocal() {
    console.log('starting local stream');
    this.localVideoActive = true;
    this.localStream.getTracks().forEach(track => {
      track.enabled = true;
    });
    this.localVideo.nativeElement.srcObject = this.localStream;
  }
  closeCall() {
    console.log("End calling...");
    if (this.peerConnection) {
      console.log('Closing the peer connection');
      this.peerConnection.ontrack = null;
      this.peerConnection.onicecandidate = null;
      this.peerConnection.oniceconnectionstatechange = null;
      this.peerConnection.onsignalingstatechange = null;

      this.peerConnection.getTransceivers().forEach(transceiver => {
        transceiver.stop();
      })

      this.peerConnection.close();

      this.inCall = false;
    }

  }

  pauseLocalVideo(): void {
    console.log('pause local stream');
    this.localStream.getTracks().forEach(track => {
      track.enabled = false;
    });
    this.localVideo.nativeElement.srcObject = undefined;

    this.localVideoActive = false;
  }

  async handleOfferMessage(msg: RTCSessionDescriptionInit) {
    console.log('handle incoming message');
    if (!this.peerConnection) {
      this.createPeerConnection();
    }

    if (!this.localStream) {
      await this.startCallLocal();
    }
    await this.peerConnection.setRemoteDescription(new RTCSessionDescription(msg))
      .then(() => {
        this.localVideo.nativeElement.srcObject = this.localStream;
        console.log("tracks:", this.localStream.getTracks());

        this.localStream.getTracks().forEach(
          track => this.peerConnection.addTrack(track, this.localStream)
        )
      })
      .then(() => {
        return this.peerConnection.createAnswer();
      })
      .then((answer) => {
        return this.peerConnection.setLocalDescription(answer);
      })
      .then(() => {
        this.inCall = true;
        return this._signalR.sendMessageCall({ type: 'answer', data: this.peerConnection.localDescription }, this.receiverId);
      })
      .then(() => {
        console.log("end create offer");
      })
  }
  async handleAnswerMessage(msg: RTCSessionDescriptionInit) {
    await this.peerConnection.setRemoteDescription(msg)
  }
  handleHangupMessage(msg: MessageCall): void {
    this.closeCall();
  }
  async handleICECandidateMessage(msg: RTCIceCandidate) {
    const candidate = new RTCIceCandidate(msg);
    await this.peerConnection.addIceCandidate(candidate).catch(this.reportError);
  }

  async call(): Promise<void> {
    this.createPeerConnection();
    this.localStream.getTracks().forEach(
      track => this.peerConnection.addTrack(track, this.localStream)
    );
    try {
      const offer = await this.peerConnection.createOffer(offerOptions);
      await this.peerConnection.setLocalDescription(offer);
      this.inCall = true;
      await this._signalR.sendMessageCall({ type: 'offer', data: offer }, this.receiverId);
    }
    catch (e: any) {
      this.handleGetUserMediaError(e);
    }
  }

  private createPeerConnection() {
    console.log("new peer connection...");
    this.peerConnection = new RTCPeerConnection(environment.RTCPeerConfiguration);
    this.peerConnection.onicecandidate = this.handleICECandidateEvent;
    this.peerConnection.oniceconnectionstatechange = this.handleICEConnectionStateChangeEvent;
    this.peerConnection.onsignalingstatechange = this.handleSignalingStateChangeEvent;
    this.peerConnection.ontrack = this.handleTrackEvent;
  }

  private reportError = (e: Error) => {
    console.log('got Error: ' + e.name);
    console.log(e);
  }
}
