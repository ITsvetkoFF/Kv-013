using System;
using System.Web.Script.Serialization;

namespace GitHubExtension.Security.WebApi.Helpers
{
    static class JSONHelper
    {

        public static string ToJSON(this object obj)
        {
            var serializer = new JavaScriptSerializer();
            try
            {
                return serializer.Serialize(obj);
            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
