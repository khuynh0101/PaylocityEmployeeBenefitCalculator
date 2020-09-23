using System.Collections.Generic;

namespace Paylocity.Data.Repositories
{
    public interface IEmployeeBenefitsRepository
    {
        bool SaveEmployeeBenefits(Employee employeeDataModel);
        List<EmployeesAndDependentsCost> GetAllEmployeesAndDependentsCost();
    }
}
