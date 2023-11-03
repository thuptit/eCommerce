using eCommerce.Shared.DataTransferObjects.Chats;
using MediatR;

namespace eCommerce.Shared.Queries.Chats;

public record GetListUserChatQuery : IRequest<IEnumerable<UserChatDto>>;