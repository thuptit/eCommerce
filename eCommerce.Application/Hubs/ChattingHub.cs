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
using Microsoft.AspNetCore.Authorization;

namespace eCommerce.Application.Hubs
{
    [Authorize]
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
            foreach (var connectionId in _connections.GetConnections(receiverId))
            {
                await Clients.Client(connectionId).SendAsync("Calling", message);
            }
        }

        public override Task OnConnectedAsync()
        {
            var idValue = Context.User.Claims.FirstOrDefault(x => x.Type == "Id").Value;
            if (int.TryParse(idValue,out int userId))
            {
                _connections.Add(userId,Context.ConnectionId);
            }
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception? exception)
        {
            var idValue = Context.User.Claims.FirstOrDefault(x => x.Type == "Id").Value;
            if (int.TryParse(idValue,out int userId))
            {
                _connections.Remove(userId,Context.ConnectionId);
            }
            return base.OnDisconnectedAsync(exception);
        }
    }

    public class CallDto
    {
        public string Type { get; set; }
        public dynamic Data { get; set; }
    }
}
