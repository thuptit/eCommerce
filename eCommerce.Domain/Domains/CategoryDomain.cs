using System.Data;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Commands.Categories;
using eCommerce.Shared.Cores.DataFilters;
using eCommerce.Shared.DataTransferObjects.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Domain.Domains;

public class CategoryDomain : BaseDomain<CategoryDomain>
{
    private readonly IRepository<Category, long> _repository;

    public CategoryDomain(IRepository<Category, long> repository, ILogger<CategoryDomain> logger) : base(logger)
    {
        _repository = repository;
    }

    public async Task Create(CreateCategoryCommand command)
    {
        _logger.LogInformation($"Insert category {command.Name}");
        if (await _repository.GetAll().AnyAsync(x => x.Name == command.Name))
        {
            throw new DataException("Name is existed");
        }
        await _repository.InsertAsync(new Category()
        {
            Name = command.Name,
            Description = command.Description
        });
        await _repository.CurrentUnitOfWork.SaveChangesAsync();
    }

    public async Task<IEnumerable<CategoryDto>> GetAll()
    {
        return await _repository.GetAll()
            .Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();
    }

    public async Task<PagingBase<CategoryDto>> GetAllPaging(GetAllPagingQuery param)
    {
        var query = _repository.GetAll()
            .Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description
            });
        return await query.GetPagingResultAsync(param);
    }
}