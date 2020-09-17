using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Data.Repositories
{
    public interface IEmployeeBenefitsRepository
    {
        bool SaveEmployeeBenefits(Employee employeeDataModel);
    }
}
