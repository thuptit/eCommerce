using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.DataTransferObjects.Chats;
using eCommerce.Shared.Queries.Chats;
using MediatR;

namespace eCommerce.Application.Chattings;

public class ChatQueryHandler : IRequestHandler<GetListUserChatQuery, IEnumerable<UserChatDto>>
{
    private readonly IRepository<GroupChat, long> _groupChatRepo;

    public ChatQueryHandler(IRepository<GroupChat, long> groupChatRepo)
    {
        _groupChatRepo = groupChatRepo;
    }
    public Task<IEnumerable<UserChatDto>> Handle(GetListUserChatQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult(Enumerable.Empty<UserChatDto>());
    }
}