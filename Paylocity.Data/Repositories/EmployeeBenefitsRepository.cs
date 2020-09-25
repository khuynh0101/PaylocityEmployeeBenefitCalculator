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
    }
}
