using System.Linq.Dynamic.Core;
using eCommerce.Application.Hubs;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Cores.Sessions;
using eCommerce.Shared.DataTransferObjects.Chats;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Application.Chattings;

public class ChatService : IChatService
{
    private readonly IRepository<PersonalChat, long> _personalChatRepository;
    private readonly IEcommerceSession _session;
    public ChatService(IRepository<PersonalChat, long> personalChatRepository, IEcommerceSession session)
    {
        _personalChatRepository = personalChatRepository;
        _session = session;
    }
    public async Task<List<UserChatDto>> GetListUserChat(long userId)
    {
        var result = await _personalChatRepository.GetAll()
            .Where(x => x.UserA_Id == userId || x.UserB_Id == userId)
            .Select(x => new UserChatDto()
            {
                PersonalChatId = x.Id,
                FriendId = x.UserA_Id == userId ? x.UserB_Id : x.UserA_Id,
                FriendName = x.UserA_Id == userId ? x.UserB.UserName : x.UserA.UserName,
                FriendAvatarUrl = x.UserA_Id == userId ? x.UserB.AvatarUrl : x.UserA.AvatarUrl,
                LastMessage = x.MessageChatPersonals.OrderByDescending(s => s.CreationTime)
                    .Select(s => new ChatMessageDto()
                    {
                        IsSeen = s.IsSeen,
                        SenderId = s.SenderId,
                        Message = s.Message,
                        SenderName = s.Sender.UserName,
                        SeenDate = s.SeenTime,
                    }).FirstOrDefault(),
            }).ToListAsync();
        foreach (var userChatDto in result)
        {
            userChatDto.IsOnline = ChattingHub._currentConnections.GetConnections(userChatDto.FriendId).Any()
                ? true
                : false;
        }

        return result;
    }

    public async Task<long> GetConversation(long friendId)
    {
        return await _personalChatRepository.GetAll()
            .Where(x => (x.UserA_Id == _session.UserId && x.UserB_Id == friendId) ||
                           (x.UserB_Id == _session.UserId && x.UserA_Id == friendId))
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<List<ChatMessageDto>> GetListMessageChat(long personalChatId)
    {
        return await _personalChatRepository.GetAll()
            .Where(x => x.Id == personalChatId)
            .SelectMany(x => x.MessageChatPersonals)
            .Select(x => new ChatMessageDto()
            {
                IsSeen = x.IsSeen,
                SeenDate = x.SeenTime,
                SenderId = x.SenderId,
                SenderName = x.Sender.UserName,
                Message = x.Message,
                AvatarUrl = x.Sender.AvatarUrl,
            })
            .ToListAsync();
    }
}