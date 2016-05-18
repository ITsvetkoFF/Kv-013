using System.Linq;
using System.Web.Http.Results;

namespace GitHubExtension.Security.Test.Extensions
{
    public static class InvalidModelStateResultExtentions
    {
        public static string GetErrorMessage(this InvalidModelStateResult model, string value)
        {
            return model.ModelState[value].Errors.First().ErrorMessage;
        }
    }
}