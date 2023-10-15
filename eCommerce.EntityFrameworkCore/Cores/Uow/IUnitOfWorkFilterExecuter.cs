using Microsoft.EntityFrameworkCore;

namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public interface IUnitOfWorkFilterExecuter
{
    void ApplyCurrentFilters(IUnitOfWorkCore unitOfWork, DbContext dbContext);
    void ApplyDisableFilters(IUnitOfWorkCore unitOfWork, string filterName);
    void ApplyEnableFilters(IUnitOfWorkCore unitOfWork, string filterName);
    void ApplyFilterParameterValue(IUnitOfWorkCore unitOfWork, string filterName, string parameterName, object value);
}