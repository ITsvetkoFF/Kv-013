using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GitHubExtension.Templates.CommunicationModels;
using GitHubExtension.Templates.Exceptions;
using Newtonsoft.Json;

namespace GitHubExtension.Templates.ExtensionMethods
{
    public static class HttpResponseMessageExtension
    {
        public static async Task<string> GetTemplatesContent(this HttpResponseMessage message)
        {
            var messageContent = await message.Content.ReadAsStringAsync();
            var dto = JsonConvert.DeserializeObject<GitHubTemplatesModel>(messageContent);
            var data = Convert.FromBase64String(dto.Content);
            var final = Encoding.UTF8.GetString(data);

            return final;
        }

        public static HttpResponseMessage CheckResponseMessage(this HttpResponseMessage response)
        {
            if (response.StatusCode == HttpStatusCode.NotFound)
                return new HttpResponseMessage(response.StatusCode);

            if (!response.IsSuccessStatusCode)
                throw new UnsuccessfullGitHubRequestException();

            return response;
        }
    }
}