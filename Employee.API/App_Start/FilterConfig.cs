using Employee.API.Filters;
using System.Web;
using System.Web.Mvc;

namespace Employee.API
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //filters.Add(new ElmahErrorAttribute());
        }
    }
}
