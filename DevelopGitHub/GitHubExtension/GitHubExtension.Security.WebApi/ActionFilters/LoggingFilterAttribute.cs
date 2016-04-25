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
        private static readonly string LogFormat = string.Format("Controller : {{0}}{0}Action : {{1}}", Environment.NewLine);

        public override void OnActionExecuting(HttpActionContext filterContext)
        {
            GlobalConfiguration.Configuration.Services.Replace(typeof(ITraceWriter), new NLogger());
            ITraceWriter trace = GlobalConfiguration.Configuration.Services.GetTraceWriter();
            trace.Info(filterContext.Request, LogFormat, "JSON", filterContext.ActionArguments);
        }
    }
}