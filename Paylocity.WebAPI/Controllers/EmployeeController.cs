using Paylocity.Data.Repositories;
using Paylocity.Logging;
using Paylocity.Service;
using Paylocity.Service.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Paylocity.WebAPI.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
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
