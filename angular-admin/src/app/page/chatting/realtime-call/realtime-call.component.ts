import { Component, ElementRef, ViewChild } from '@angular/core';
import { WebRtcService } from 'src/core/services/web-rtc.service';
import { environment } from 'src/environments/environment';
import { RtcEventHandler, RtcHandlerMessage } from './types/rtc-handler';
import { MessageCall } from './types/message-call';

const offerOptions = {
  audio: true,
  video: true
};

const mediaConstraints = {
  audio: true,
  video: { width: 1280, height: 720 }
  // video: {width: 1280, height: 720} // 16:9
  // video: {width: 960, height: 540}  // 16:9
  // video: {width: 640, height: 480}  //  4:3
  // video: {width: 160, height: 120}  //  4:3
};

@Component({
  selector: 'app-realtime-call',
  templateUrl: './realtime-call.component.html',
  styleUrls: ['./realtime-call.component.scss']
})
export class RealtimeCallComponent
  implements RtcHandlerMessage,
  RtcEventHandler {
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
      });
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
  private startCallLocal() {
    console.log('starting local stream');
    this.localStream.getVideoTracks().forEach(track => {
      track.enabled = true;
    });
    this.localVideo.nativeElement.srcObject = this.localStream;
  }
  private closeCall() {
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
        this._webRtcService.sendMessage({ type: 'answer', data: this.peerConnection.localDescription });
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
