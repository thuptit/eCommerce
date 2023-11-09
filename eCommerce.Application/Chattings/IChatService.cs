using eCommerce.Shared.DataTransferObjects.Chats;
using Volo.Abp.DependencyInjection;

namespace eCommerce.Application.Chattings;

public interface IChatService : IScopedDependency
{
    Task<List<UserChatDto>> GetListUserChat(long userId);
    Task<long> GetConversation(long friendId);
    Task<List<ChatMessageDto>> GetListMessageChat(long personalChatId);
}