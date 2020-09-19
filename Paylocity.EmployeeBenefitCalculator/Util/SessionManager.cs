using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Paylocity.EmployeeBenefitCalculator.Util
{
    public static class SessionManager<T>
    {
        public static void Save(string sessionKey, T objectToSave)
        {
            HttpContext.Current.Session[sessionKey] = objectToSave;
        }

        public static T Retrieve(string sessionKey)
        {
            return (T) HttpContext.Current.Session[sessionKey];
        }

        public static void Remove(string sessionKey)
        {
            HttpContext.Current.Session.Remove(sessionKey);
        }
    }
}