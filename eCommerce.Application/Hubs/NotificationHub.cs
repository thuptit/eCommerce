using eCommerce.Shared.Cores.DependencyInjections;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Application.Hubs
{
    public class NotificationHub : Hub, ISingletonDependency
    {
        private ConcurrentDictionary<long, string> userConnection = new ConcurrentDictionary<long, string>();
        public async Task SendMessage(long userId, string message)
        {
            await Clients.User(userConnection[userId]).SendAsync(message);
        }
        public string GetConnectionId(long userId) 
        {
            userConnection.TryAdd(userId, Context.ConnectionId);
            return Context.ConnectionId;
        }
    }
}
