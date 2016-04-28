using System;
using System.Text;
using GitHubExtension.Templates.CommunicationModels;
using Newtonsoft.Json;

namespace GitHubExtension.Templates.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string GetRequestCreateContent(this string content, string gitHubMessage)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);

            string encodedContent = Convert.ToBase64String(buffer);

            CreateUpdateTemplateModel model = new CreateUpdateTemplateModel() { Content = encodedContent, Message = gitHubMessage };

            var requestContent = JsonConvert.SerializeObject(model);

            return requestContent;
        }

        public static string GetRequestUpdateContent(this string content, string gitHubMessage, string sha)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(content);

            string encodedContent = Convert.ToBase64String(buffer);

            CreateUpdateTemplateModel model = new CreateUpdateTemplateModel() 
            {
                Content = encodedContent, 
                Message = gitHubMessage, 
                Sha = sha
            };

            var requestContent = JsonConvert.SerializeObject(model);

            return requestContent;
        }
    }
}
