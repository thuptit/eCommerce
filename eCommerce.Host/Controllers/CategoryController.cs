using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using eCommerce.Shared;
using eCommerce.Shared.Commands.Categories;
using eCommerce.Shared.Cores.DataFilters;
using eCommerce.Shared.DataTransferObjects.Categories;
using eCommerce.Shared.Queries.Categories;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

namespace eCommerce.Host.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IDistributedCache _cache;
        public CategoryController(IMediator mediator, IDistributedCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }
        [HttpGet]
        public async Task<IEnumerable<CategoryDto>> Get()
        {
            return await _mediator.Send(new GetAllCategoryQuery());
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<string> Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Authorize("admin")]
        public async Task<bool> Post([FromBody] CreateCategoryCommand request)
        {
            return await _mediator.Send(request);
        }

        // PUT: api/Category/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/Category/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        // GET: api/Category/GetAllPaging
        [HttpPost("GetAllPaging")]
        public async Task<PagingBase<CategoryDto>> GetAllPaging([FromBody]GetAllPagingQuery query)
        {
            return await _mediator.Send(query);
        }

        [HttpPost("redis-cache")]
        public async Task AddCache()
        {
            _cache.Set("category",Encoding.UTF8.GetBytes("Hello"));
        }
        [HttpGet("redis-cache")]
        public async Task<string> GetCachce()
        {
            return Encoding.UTF8.GetString(await _cache.GetAsync("category"));
        }
    }
}
