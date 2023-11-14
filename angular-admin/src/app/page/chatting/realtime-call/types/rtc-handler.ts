import { MessageCall } from "./message-call";

export interface RtcHandlerMessage {
    handleOfferMessage(msg: RTCSessionDescriptionInit): void;
    handleAnswerMessage(msg: RTCSessionDescriptionInit): void;
    handleHangupMessage(msg: MessageCall): void;
    handleICECandidateMessage(msg: RTCIceCandidate): void;
}

export interface RtcEventHandler {
    handleICECandidateEvent: Function;
    handleICEConnectionStateChangeEvent: Function;
    handleSignalingStateChangeEvent: Function;
    handleTrackEvent: Function;
}