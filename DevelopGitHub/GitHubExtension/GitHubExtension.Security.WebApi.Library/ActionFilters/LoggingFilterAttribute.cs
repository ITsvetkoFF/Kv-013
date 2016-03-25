using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;
using GitHubExtension.Security.WebApi.Library.Helpers;

namespace GitHubExtension.Security.WebApi.Library.ActionFilters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(filterContext.Request,
                "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName +
                Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, "JSON",
                filterContext.ActionArguments);
        }
    }
}
