using System.Data;
using eCommerce.Domain.Domains;
using eCommerce.Domain.Repositories;
using eCommerce.EntityFrameworkCore.Entities;
using eCommerce.Shared.Commands.Categories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerce.Application.Categories;

public class CategoryCommandHandler : IRequestHandler<CreateCategoryCommand,string>,
    IRequestHandler<DeleteCategoryCommand, string>
{
    private readonly IRepository<Category, long> _repository;
    private readonly ILogger<CategoryCommandHandler> _logger;
    public CategoryCommandHandler(IRepository<Category, long> repository, ILogger<CategoryCommandHandler> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    public async Task<string> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Insert category {request.Name}");
        if (await _repository.GetAll().AnyAsync(x => x.Name == request.Name))
        {
            throw new DataException("Name is existed");
        }
        await _repository.InsertAsync(new Category()
        {
            Name = request.Name,
            Description = request.Description
        });
        await _repository.CurrentUnitOfWork.SaveChangesAsync();
        return "Created Successfully";
    }

    public async Task<string> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id);
        return "Delete Successfully";
    }
}