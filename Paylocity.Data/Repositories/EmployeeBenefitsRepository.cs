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
