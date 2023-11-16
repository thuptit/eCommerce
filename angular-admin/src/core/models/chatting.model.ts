import { BaseDataDialog } from "./base.model";

export interface CreateConversationDataDialogModel extends BaseDataDialog<null> {
}

export interface UserChatModel {
    personalChatId: number;
    friendId: number;
    friendName: string;
    friendAvatarUrl: string;
    lastMessage: MessageChatModel;
    isOnline: boolean;
}

export interface MessageChatModel {
    senderId: number;
    senderName: string;
    isSeen: boolean;
    seenDate: Date;
    avatarUrl: string;
    message: string;
}

export interface SendMessageChatModel {
    senderId: number;
    personalChatId: number;
    message: string;
    receiverId: number;
}