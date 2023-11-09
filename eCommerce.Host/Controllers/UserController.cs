using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Application.Users;
using eCommerce.Application.Users.Dtos;
using eCommerce.Shared.Commands.Users;
using eCommerce.Shared.Cores.DataFilters;
using eCommerce.Shared.DataTransferObjects.Users;
using eCommerce.Shared.Queries.Users;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IUserService _userService;
        public UserController(IMediator mediator, IUserService userService)
        {
            _mediator = mediator;
            _userService = userService;
        }
        [HttpGet]
        [Authorize]
        public async Task<string> Get()
        {
            await Task.CompletedTask;
            return "Hêlo";
        }

        [HttpPost]
        [Authorize("Admin")]
        public async Task<string> CreateStaff()
        {
            await Task.CompletedTask;
            return string.Empty;
        }
        [Authorize]
        [HttpPost("GetAllPaging")]
        public async Task<PagingBase<UserPageDto>> GetlAllPaging([FromBody] GetAllUserPagingQuery request)
        {
            return await _mediator.Send(request);
        }

        [Authorize("Admin")]
        [HttpPost("CreateUser")]
        public async Task<string> CreateUser([FromForm] CreateUserCommand command)
        {
            return await _mediator.Send(command);
        }

        [Authorize("Admin")]
        [HttpGet("GetUserByName")]
        public async Task<List<AutoCompleteUserDto>> GetUserByName(string name)
        {
            return await _userService.GetUserByName(name);
        }

        [Authorize("Admin")]
        [HttpGet("GetAllUser")]
        public async Task<List<AutoCompleteUserDto>> GetAllUser()
        {
            return await _userService.GetAllUser();
        }
    }
}
