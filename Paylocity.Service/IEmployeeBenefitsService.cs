
using Paylocity.Service.Models;

namespace Paylocity.Service
{
    public interface IEmployeeBenefitsService
    {
        bool Save(Employee employeeModel);

        BenefitsSummary CalculateBenefitsCost(Employee employeeModel);
    }
}
