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
    public class ChattingHub : Hub
    {
        private readonly IIocManager _iocManager;
        private readonly static ConnectionMapping<long> _connections = 
            new ConnectionMapping<long>();
        public ChattingHub(IIocManager iocManager)
        {
            _iocManager = iocManager;
        }
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
            foreach (var connectionId in _connections.GetConnections(message.ReceiverId))
            {
                await Clients.Client(connectionId).SendAsync("ReceivedMessage", message);
            }
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

        public async Task SendCall(CallDto message, long receiverId)
        {
            // await Clients.User(_clientManager.Clients[receiverId])
            //     .SendAsync("Calling", message);
        }

        public string GetConnectionId(long userId) 
        {
            _connections.Add(userId, Context.ConnectionId);
            return Context.ConnectionId;
        }
    }

    public class CallDto
    {
        public string Type { get; set; }
        public dynamic Data { get; set; }
    }
}
