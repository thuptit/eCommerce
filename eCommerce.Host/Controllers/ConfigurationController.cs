using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerce.Shared.DataTransferObjects.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigurationController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public ConfigurationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public async Task<ConfigurationDto> Get()
        {
            await Task.CompletedTask;
            return new ConfigurationDto()
            {
                DefaultAvatar = _configuration["App:Host"] + "avatars/default.jpg"
            };
        }
    }
}
