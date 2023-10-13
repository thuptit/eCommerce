using MediatR;

namespace eCommerce.Shared.Commands.Categories;

public class DeleteCategoryCommand : IRequest<string>
{
    public DeleteCategoryCommand(long id)
    {
        Id = id;
    }
    public long Id { get; set; }
}