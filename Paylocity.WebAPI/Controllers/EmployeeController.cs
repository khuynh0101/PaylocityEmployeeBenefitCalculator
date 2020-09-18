using Paylocity.Data.Repositories;
using Paylocity.Logging;
using Paylocity.Service;
using Paylocity.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Paylocity.WebAPI.Controllers
{
    public class EmployeeController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage CalculateBenefitsCost([FromBody] Employee employee)
        {
            try
            {
                EmployeeBenefitsService service = new EmployeeBenefitsService(new EmployeeBenefitsRepository());
                BenefitsSummary summary = service.CalculateBenefitsCost(employee);
                return Request.CreateResponse(HttpStatusCode.OK, new { summary });
            }
            catch (Exception ex)
            {
                Log.LogMessage(TracingLevel.ERROR, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error");
            }
        }

        [HttpPost]
        public HttpResponseMessage Save([FromBody] Employee employee)
        {
            try
            {
                EmployeeBenefitsService service = new EmployeeBenefitsService(new EmployeeBenefitsRepository());
                //just in case website doesn't send the employee's benefits information
                if (employee.BenefitsSummary == null)
                    employee.BenefitsSummary = service.CalculateBenefitsCost(employee);
                bool isSaved = service.Save(employee);
                return Request.CreateResponse(HttpStatusCode.OK, new { isSaved });
            }
            catch (Exception ex)
            {
                Log.LogMessage(TracingLevel.ERROR, ex);
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error");
            }
        }
    }
}
