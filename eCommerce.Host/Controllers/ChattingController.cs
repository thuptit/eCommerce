using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Application.Chattings;
using eCommerce.Shared.DataTransferObjects.Chats;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChattingController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChattingController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpGet("GetListUserChat/{userId}")]
        public async Task<List<UserChatDto>> GetListUserChat(long userId)
        {
            return await _chatService.GetListUserChat(userId);
        }

        [HttpGet("GetConversation/{friendId}")]
        public async Task<long> GetConversation(long friendId)
        {
            return await _chatService.GetConversation(friendId);
        }

        [HttpGet("GetListMessageChat/{personalChatId}")]
        public async Task<List<ChatMessageDto>> GetListMessageChat(long personalChatId)
        {
            return await _chatService.GetListMessageChat(personalChatId);
        }
    }
}
