namespace eCommerce.Shared.DataTransferObjects.Chats;

public class UserChatDto
{
    public long PersonalChatId { get; set; }
    public long FriendId { get; set; }
    public string FriendName { get; set; }
    public string FriendAvatarUrl { get; set; }
    public ChatMessageDto? LastMessage { get; set; }
}

public class ChatMessageDto
{
    public long SenderId { get; set; }
    public string Message { get; set; }
    public string SenderName { get; set; }
    public string AvatarUrl { get; set; }
    public bool IsSeen { get; set; }
    public DateTime? SeenDate { get; set; }
}
