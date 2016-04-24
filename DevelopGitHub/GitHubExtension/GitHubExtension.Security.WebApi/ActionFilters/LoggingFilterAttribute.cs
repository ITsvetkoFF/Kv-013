using System;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.Tracing;

using GitHubExtension.Security.WebApi.Helpers;

namespace GitHubExtension.Security.WebApi.ActionFilters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            var trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(
                filterContext.Request, 
                "Controller : " + filterContext.ControllerContext.ControllerDescriptor.ControllerType.FullName
                + Environment.NewLine + "Action : " + filterContext.ActionDescriptor.ActionName, 
                "JSON", 
                filterContext.ActionArguments);
        }
    }
}