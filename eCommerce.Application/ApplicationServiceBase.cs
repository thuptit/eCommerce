using eCommerce.EntityFrameworkCore.UnitOfWorks;

namespace eCommerce.Application;

public class ApplicationServiceBase : IApplicationService
{
    public IUnitOfWork CurrentUnitOfWork { get => _unitOfWork; set => _unitOfWork = value; }
    private IUnitOfWork _unitOfWork;
}

public interface IApplicationService
{
}
