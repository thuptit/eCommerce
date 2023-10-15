using Microsoft.EntityFrameworkCore;

namespace eCommerce.EntityFrameworkCore.Cores.Uow;

public class EfDynamicFiltersUnitOfWorkFilterExecuter : IUnitOfWorkFilterExecuter
{
    public void ApplyCurrentFilters(IUnitOfWorkCore unitOfWork, DbContext dbContext)
    {
        throw new NotImplementedException();
    }

    public void ApplyDisableFilters(IUnitOfWorkCore unitOfWork, string filterName)
    {
        throw new NotImplementedException();
    }

    public void ApplyEnableFilters(IUnitOfWorkCore unitOfWork, string filterName)
    {
        throw new NotImplementedException();
    }

    public void ApplyFilterParameterValue(IUnitOfWorkCore unitOfWork, string filterName, string parameterName, object value)
    {
        throw new NotImplementedException();
    }
}