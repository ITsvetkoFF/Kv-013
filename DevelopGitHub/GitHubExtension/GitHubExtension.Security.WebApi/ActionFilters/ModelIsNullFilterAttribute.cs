using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GitHubExtension.Security.WebApi.ActionFilters
{
    public class ModelIsNullFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (IsAnyArgumentNull(actionContext))
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    HttpStatusCode.BadRequest,
                    "Arguments cannot be null");
            }
        }

        private bool IsAnyArgumentNull(HttpActionContext actionContext)
        {
            return actionContext.ActionArguments.Any(v => v.Value == null);
        }
    }
}
