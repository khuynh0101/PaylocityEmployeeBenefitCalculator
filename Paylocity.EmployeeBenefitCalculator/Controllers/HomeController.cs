using Paylocity.Data.Repositories;
using Paylocity.Logging;
using Paylocity.Service;
using Paylocity.Service.ModelBinder;
using Paylocity.Service.Models;
using System;
using System.Web.Mvc;

namespace Paylocity.EmployeeBenefitCalculator.Controllers
{
    public class HomeController : Controller
    {
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
                //calculating the benefits cost summary
                employeeModel.BenefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(employeeModel);
            }
            return View(employeeModel);
        }
        [HttpPost]
        public ActionResult Save([ModelBinder(typeof(EmployeeBinder))] Employee employeeModel)
        {
            if (ModelState.IsValid)
            {
                //saving the employee/dependents
                EmployeeBenefitsService service = new EmployeeBenefitsService(new EmployeeBenefitsRepository());
                TempData["IsSaved"] = service.Save(employeeModel);               
            }
            return RedirectToAction("Index");
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