using Paylocity.Data.Repositories;
using Paylocity.Service;
using Paylocity.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Paylocity.EmployeeBenefitCalculator.Controllers
{
    public class ViewController : Controller
    {
        // GET: View
        public ActionResult Index()
        {
            EmployeeBenefitsService service = new EmployeeBenefitsService(new EmployeeBenefitsRepository());
            List<EmployeesDependents> employeesDependents = service.GetAllEmployeesAndDependentsCost();
            return View(employeesDependents);
        }
    }
}