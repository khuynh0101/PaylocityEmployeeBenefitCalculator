using Paylocity.Data.Repositories;
using Paylocity.Logging;
//using Paylocity.EmployeeBenefitCalculator.Models;
using Paylocity.Service;
using Paylocity.Service.ModelBinder;
using Paylocity.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                employeeModel.BenefitsSummary = new EmployeeBenefitsService(new EmployeeBenefitsRepository()).CalculateBenefitsCost(employeeModel);
                Session["employee"] = employeeModel;
            }
            return View();
        }

        public ActionResult Save()
        {
            if (ModelState.IsValid && Session["employee"] != null)
            {
                EmployeeBenefitsService service = new EmployeeBenefitsService(new EmployeeBenefitsRepository());
                bool isSaved = service.Save(Session["employee"] as Employee);
                if (isSaved)
                {
                    Session.Remove("employee");
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
            if (Session["employee"] != null)
                return PartialView("_Summary", (Session["employee"] as Employee));

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