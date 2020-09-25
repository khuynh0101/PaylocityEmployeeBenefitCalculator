using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Paylocity.Data.Repositories
{
   public class EmployeeBenefitsRepository : IEmployeeBenefitsRepository
    {
        private PaylocityEntities _context = new PaylocityEntities();
        public bool SaveEmployeeBenefits(Employee employeeDataModel)
        {
            int numRec = 0;
            using (_context)
            {
                _context.Employees.Add(employeeDataModel);
                numRec = _context.SaveChanges();
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
