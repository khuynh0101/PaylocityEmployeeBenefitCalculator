using System.Web;
using Paylocity.Common.Cache;

namespace Paylocity.EmployeeBenefitCalculator.Util
{
    public class SessionManager<TObject> :  ICacheManager<string, TObject>
    {
        public void Save(string key, TObject objectToSave)
        {
            HttpContext.Current.Session[key] = objectToSave;
        }

        public TObject Retrieve(string key)
        {
            return (TObject)HttpContext.Current.Session[key];
        }

        public void Remove(string key)
        {
            HttpContext.Current.Session.Remove(key);
        }
    }

}