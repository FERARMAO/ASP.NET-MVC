using System.Web;
using System.Web.Mvc;

namespace Vidly
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new AuthorizeAttribute()); //This filter restrict access unless u have an account for all MVC controllers. But for API controllers, we need to add [Authorize] where we want to restrict!
            filters.Add(new RequireHttpsAttribute()); //With this, my application endpoints will no longer be available on an Http Channel! 
        }
    }
}
