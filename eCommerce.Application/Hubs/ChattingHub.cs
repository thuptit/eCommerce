using eCommerce.Shared.Cores.DependencyInjections;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.DataTransferObjects.Chats;

namespace eCommerce.Application.Hubs
{
    public class ChattingHub : Hub, ISingletonDependency
    {
        private readonly IIocManager _iocManager;
        public ChattingHub(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }
        private ConcurrentDictionary<long, string> _userConnection = new ConcurrentDictionary<long, string>();
        public async Task<long> SendMessage(SendMessageChatDto message)
        {
            if (message.PersonalChatId == 0)
            {
                var _chatPersonalRepository = _iocManager.Resolve<IRepository<PersonalChat, long>>();
                message.PersonalChatId = await _chatPersonalRepository.InsertAndGetIdAsync(new PersonalChat()
                {
                    UserA_Id = message.SenderId,
                    UserB_Id = message.ReceiverId
                });
            }
            await Clients.Client(_userConnection[message.ReceiverId])
                .SendAsync("ReceivedMessage",message);
            var _chatMessageRepository = _iocManager.Resolve<IRepository<MessageChatPersonal, long>>();
            await _chatMessageRepository.InsertAsync(new MessageChatPersonal()
            {
                Message = message.Message,
                PersonalChatId = message.PersonalChatId,
                SenderId = message.SenderId
            });
            await _chatMessageRepository.CurrentUnitOfWork.SaveChangesAsync();
            return message.PersonalChatId;
        }

        public override Task OnConnectedAsync()
        {
            
            return base.OnConnectedAsync();
        }

        public string GetConnectionId(long userId) 
        {
            _userConnection.TryAdd(userId, Context.ConnectionId);
            return Context.ConnectionId;
        }
    }
}
