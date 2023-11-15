export const environment = {
    production: false,
    apiUrl: 'https://localhost:5001',
    clientId: '21339795970-ua1dmt5pm89ce1tn4k00j152bc1c3khq.apps.googleusercontent.com',
    RTCPeerConfiguration: {
        iceServers: [
            {
                urls: 'stun:stun4.l.google.com:19302'
            }
        ]
    }
}