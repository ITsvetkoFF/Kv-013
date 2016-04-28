using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using GitHubExtension.Templates.CommunicationModels;

using Newtonsoft.Json;

namespace GitHubExtension.Templates.ExtensionMethods
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<string> GetTemplatesContent(this HttpResponseMessage message)
        {
            var messageContent = await message.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<GitHubTemplatesModel>(messageContent);
            var data = Convert.FromBase64String(model.Content);
            var final = Encoding.UTF8.GetString(data);

            return final;
        }
    }
}