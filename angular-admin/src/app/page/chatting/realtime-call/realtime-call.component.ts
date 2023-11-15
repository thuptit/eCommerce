import { Component, ElementRef, Input, ViewChild } from '@angular/core';
import { WebRtcService } from 'src/core/services/web-rtc.service';
import { environment } from 'src/environments/environment';
import { RtcEventHandler, RtcHandlerMessage } from './types/rtc-handler';
import { MessageCall } from './types/message-call';

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
export class RealtimeCallComponent implements RtcHandlerMessage, RtcEventHandler {
  @Input() receiverId!: number;
  private peerConnection!: RTCPeerConnection;
  private localStream!: MediaStream;

  inCall: boolean = false;
  localVideoActive: boolean = false;

  @ViewChild('local_video') localVideo!: ElementRef;
  @ViewChild('received_video') remoteVideo!: ElementRef;

  constructor(private _webRtcService: WebRtcService) {
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
      this._webRtcService.sendMessage({
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
    console.log(event);
    this.remoteVideo.nativeElement.srcObject = event.streams[0];
  };
  //end event

  //request media devices
  private async requestMediaDevice() {
    try {
      this.localStream = await navigator.mediaDevices.getUserMedia(mediaConstraints);
    }
    catch (e) {
      console.error(e);
    }
  }
  hangUp(): void {
    this._webRtcService.sendMessage({ type: 'hangup', data: '' }, this.receiverId);
    this.closeCall();
  }
  async startCallLocal() {
    console.log('starting local stream');
    await this.requestMediaDevice();
    this.localVideoActive = true;
    this.localStream.getVideoTracks().forEach(track => {
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
  handleOfferMessage(msg: RTCSessionDescriptionInit): void {
    console.log('handle incoming message');
    if (!this.peerConnection) {
      this.createPeerConnection();
    }

    if (!this.localStream) {
      this.startCallLocal();
    }
    this.peerConnection.setRemoteDescription(new RTCSessionDescription(msg))
      .then(() => {
        this.localVideo.nativeElement.srcObject = this.localStream;
        this.localStream.getTracks().forEach(
          track => this.peerConnection.addTrack(track)
        )
      })
      .then(() => {
        return this.peerConnection.createAnswer();
      })
      .then((answer) => {
        return this.peerConnection.setLocalDescription(answer);
      })
      .then(() => {
        this._webRtcService.sendMessage({ type: 'answer', data: this.peerConnection.localDescription }, this.receiverId);
        this.inCall = true;
      })
  }
  handleAnswerMessage(msg: RTCSessionDescriptionInit): void {
    this.peerConnection.setRemoteDescription(msg)
  }
  handleHangupMessage(msg: MessageCall): void {
    this.closeCall();
  }
  handleICECandidateMessage(msg: RTCIceCandidate): void {
    const candidate = new RTCIceCandidate(msg);
    this.peerConnection.addIceCandidate(candidate).catch(this.reportError);
  }

  async call(): Promise<void> {
    this.createPeerConnection();
    this.localStream.getTracks().forEach(
      track => this.peerConnection.addTrack(track)
    );
    try {
      const offer = await this.peerConnection.createOffer(offerOptions);
      await this.peerConnection.setLocalDescription(offer);
      this.inCall = true;
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
