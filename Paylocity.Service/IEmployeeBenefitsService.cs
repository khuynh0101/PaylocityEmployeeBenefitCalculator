using Paylocity.Service.Models;
using System.Collections.Generic;

namespace Paylocity.Service
{
    public interface IEmployeeBenefitsService
    {
        bool Save(Employee employeeModel);

        BenefitsSummary CalculateBenefitsCost(Employee employeeModel);

        List<EmployeesDependents> GetAllEmployeesAndDependentsCost();
    }
}
