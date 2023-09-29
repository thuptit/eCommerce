using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<string> Get()
        {
            await Task.CompletedTask;
            return "Hêlo";
        }

        [HttpGet]
        [Authorize("Admin")]
        public async Task<string> CreateStaff()
        {
            
        } 
    }
}
