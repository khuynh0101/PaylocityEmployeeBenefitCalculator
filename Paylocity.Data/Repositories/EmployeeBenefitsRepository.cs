using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Data.Repositories
{
   public class EmployeeBenefitsRepository : IEmployeeBenefitsRepository
    {
        public bool SaveEmployeeBenefits(Employee employeeDataModel)
        {
            PaylocityEntities context = new PaylocityEntities();            
            context.Employees.Add(employeeDataModel);

            int numRec = context.SaveChanges();
            return numRec > 0;
        }
    }
}
