using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.EntityFrameworkCore.UnitOfWorks;
using eCommerce.Shared.Cores.Caches;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ICacheService _cacheService;

        public TestController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
        [HttpGet]
        [Authorize(Policy = "admin")]
        [UnitOfWork(isTransactional: false)]
        public async Task<string> Get()
        {
            await Task.CompletedTask;
            return "Hello";
        }

        [HttpGet("SetKey")]
        public void SetKey()
        {
            _cacheService.Set("int",6);
        }
        [HttpGet("GetKey")]
        public int GetKey()
        {
            var value = _cacheService.Get("int");
            return int.Parse(value);
        }
    }
}
