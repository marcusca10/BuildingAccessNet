using System.Web;
using System.Web.Mvc;

namespace Marcusca10.Samples.BuildingAccessNet.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
