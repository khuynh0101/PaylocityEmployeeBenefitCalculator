using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Paylocity.Data.Repositories
{
   public class EmployeeBenefitsRepository : IEmployeeBenefitsRepository
    {
        public bool SaveEmployeeBenefits(Employee employeeDataModel)
        {
            int numRec = 0;
            using (PaylocityEntities context = new PaylocityEntities())
            { 
                context.Employees.Add(employeeDataModel);
                numRec = context.SaveChanges();
            }            
            return numRec > 0;
        }

        public List<EmployeesAndDependentsCost> GetAllEmployeesAndDependentsCost()
        {
            List<EmployeesAndDependentsCost> data = null;
            using (PaylocityEntities context = new PaylocityEntities())
            {
                data = (from p in context.GetAllEmployeesAndDependentsCost()
                        select p).ToList();
            }
            return data;
        }
    }
}
