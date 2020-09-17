
using ViewModel = Paylocity.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Service
{
    public interface IEmployeeBenefitsService
    {
        bool Save(ViewModel.Employee employeeModel);

        ViewModel.BenefitsSummary CalculateBenefitsCost(ViewModel.Employee employeeModel);
    }
}
