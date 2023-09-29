using System.Data;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Commands.Categories;
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
            Name = command.Name
        });
    }
}