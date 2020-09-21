using Paylocity.Service.Models;
using System;
using System.Collections.Specialized;
using System.Linq;
using System.Web.Mvc;

namespace Paylocity.Service.ModelBinder
{
    public class EmployeeBinder : IModelBinder
    { 
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            NameValueCollection collection = controllerContext.HttpContext.Request.Form;
            Employee employeeModel = new Employee() { Name = collection["Name"] };
            string dependents = collection["Dependent"];
            if (!string.IsNullOrWhiteSpace(dependents))
            {
                employeeModel.Dependents = (from d in dependents.Split(',')
                                            where !string.IsNullOrWhiteSpace(d)
                                            select new Dependent() { Name = d }).ToList();
            }
            return employeeModel;
        }
    }
}
