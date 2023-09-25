using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Shared.Commands.Authentications;
using eCommerce.Shared.DataTransferObjects.Authentications;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<bool> Register(RegisterAccountCommand command)
        {
            return await _mediator.Send(command);
        }

        [HttpPost("LoginAdminSite")]
        [AllowAnonymous]
        public async Task<LoginResponseDto> LoginAdminSite(LoginAdminSiteCommand command)
        {
            var login = await _mediator.Send(command);

            return new LoginResponseDto()
            {
                UserName = login.UserName,
                Email = login.Email,
                Id = login.Id,
                //AccessToken = 
            };
        }

        private string GenerateAccessToken()
        {
            return string.Empty;
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task Login()
        {
            
        }
    }
}
