using System.Data;
using eCommerce.Domain.Domains;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Commands.Categories;
using eCommerce.Shared.Cores.DataFilters;
using eCommerce.Shared.Queries.Categories;
using eCommerce.Shared.DataTransferObjects.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Categories;

public class CategoryQueryHandler : IRequestHandler<GetAllCategoryQuery,IEnumerable<CategoryDto>>,
    IRequestHandler<GetAllPagingQuery, PagingBase<CategoryDto>>
{
    private readonly IRepository<Category, long> _repository;
    private readonly ILogger<CategoryQueryHandler> _logger;
    public CategoryQueryHandler(IRepository<Category, long> repository, ILogger<CategoryQueryHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll()
            .Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<PagingBase<CategoryDto>> Handle(GetAllPagingQuery request, CancellationToken cancellationToken)
    {
        var query = _repository.GetAll()
            .Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
        return await query.GetPagingResultAsync(request);
    }
}