export const GlobalString = {
    CREATE: 'Create',
    EDIT: 'Edit',
    SAVE: 'Save',
    CLOSE: 'Close',
    YES: 'Yes',
    CANCEL: 'Cancel'
}
export enum DialogMode {
    CREATE,
    EDIT,
    DELETE
}

export const WebRTCConstant = {
    //Wowza WebRTC constants
    WEBRTC_CONSTRAINTS: { audio: true, video: false },
    ICE_SERVERS: [{ url: 'stun:numb.viagenie.ca' }, {
        url: 'turn:numb.viagenie.ca',
        username: 'shahzad@fms-tech.com',
        credential: 'P@ssw0rdfms'
    }],
    //SERVER_URL : "", //"wss://localhost.streamlock.net/webrtc-session.json", set it from the hub connection
    WOWZA_APPLICATION_NAME: "webrtc",
    WOWZA_STREAM_NAME: "", //"myStream", set it from the user name 
    WOWZA_SESSION_ID_EMPTY: "[empty]",

    STATUS_OK: 200,
    STATUS_APPLICATION_FAILURE: 500,
    STATUS_ERROR_STARTING_APPLICATION: 501,
    STATUS_ERROR_STREAM_NOT_RUNNING: 502,
    STATUS_STREAMNAME_INUSE: 503,
    STATUS_STREAM_NOT_READY: 504,
    STATUS_ERROR_CREATE_SDP_OFFER: 505,
    STATUS_ERROR_CREATING_RTP_STREAM: 506,
    STATUS_WEBRTC_SESSION_NOT_FOUND: 507,
    STATUS_ERROR_DECODING_SDP_DATA: 508,
    STATUS_ERROR_SESSIONID_NOT_SPECIFIED: 509,

    CODEC_AUDIO_UNKNOWN: -1,
    CODEC_AUDIO_PCM_BE: 0x00,
    CODEC_AUDIO_PCM_SWF: 0x01,
    CODEC_AUDIO_AC3: 0x01, //TODO steal this slot
    CODEC_AUDIO_MP3: 0x02,
    CODEC_AUDIO_PCM_LE: 0x03,
    CODEC_AUDIO_NELLYMOSER_16MONO: 0x04,
    CODEC_AUDIO_NELLYMOSER_8MONO: 0x05,
    CODEC_AUDIO_NELLYMOSER: 0x06,
    CODEC_AUDIO_G711_ALAW: 0x07,
    CODEC_AUDIO_G711_MULAW: 0x08,
    CODEC_AUDIO_RESERVED: 0x09,
    CODEC_AUDIO_VORBIS: 0x09, //TODO steal this slot
    CODEC_AUDIO_AAC: 0x0a,
    CODEC_AUDIO_SPEEX: 0x0b,
    CODEC_AUDIO_OPUS: 0x0c,
    CODEC_AUDIO_MP3_8: 0x0f,
}