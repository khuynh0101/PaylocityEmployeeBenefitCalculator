using System;
using System.Web.Mvc;
using Paylocity.Common.Cache;
using Paylocity.Data.Repositories;
using Paylocity.EmployeeBenefitCalculator.Util;
using Paylocity.Logging;
using Paylocity.Service;
using Paylocity.Service.ModelBinder;
using Paylocity.Service.Models;


namespace Paylocity.EmployeeBenefitCalculator.Controllers
{
    public class HomeController : Controller
    {
        private ICacheManager<string, Employee> SessionManager = new SessionManager<Employee>();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([ModelBinder(typeof(EmployeeBinder))] Employee employeeModel)
        {
            if (ModelState.IsValid)
            {                                
                employeeModel.BenefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(employeeModel);
                SessionManager.Save("employee", employeeModel);
            }
            return View();
        }

        public ActionResult Save()
        {
            Employee employee = SessionManager.Retrieve("employee");
            if (ModelState.IsValid && employee != null)
            {
                EmployeeBenefitsService service = new EmployeeBenefitsService(new EmployeeBenefitsRepository());
                bool isSaved = service.Save(employee);
                if (isSaved)
                {
                    SessionManager.Remove("employee");
                    TempData["IsSaved"] = true;
                }
                else
                {
                    //used by partial view
                    TempData["IsSaved"] = false;
                }
            }
            return RedirectToAction("Index");
        }

        [ChildActionOnly]
        public ActionResult Summary()
        {
            Employee employee = SessionManager.Retrieve("employee");
            if (employee != null)
                return PartialView("_Summary", employee);

            return PartialView("_Summary");
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            Exception exception = filterContext.Exception;
            //Logging the Exception
            Log.LogMessage(TracingLevel.ERROR, exception);
            filterContext.ExceptionHandled = true;


            var Result = this.View("Error", new HandleErrorInfo(exception,
                filterContext.RouteData.Values["controller"].ToString(),
                filterContext.RouteData.Values["action"].ToString()));

            filterContext.Result = Result;
        }
    }
}