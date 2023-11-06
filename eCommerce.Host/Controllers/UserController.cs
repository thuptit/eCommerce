using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
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
    }
}
