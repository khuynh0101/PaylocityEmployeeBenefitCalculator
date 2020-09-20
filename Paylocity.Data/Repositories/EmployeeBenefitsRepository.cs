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
    }
}
