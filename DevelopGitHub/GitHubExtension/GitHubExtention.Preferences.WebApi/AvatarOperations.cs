using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GitHubExtention.Preferences.WebApi.Constants;
using Microsoft.WindowsAzure.Storage.Blob;
using GitHubExtention.Preferences.WebApi.Queries; 

namespace GitHubExtention.Preferences.WebApi
{
    public class AvatarOperations
    {
        public static string GetNewAvatarUrl(IAzureContainerQuery container, string url, string userName)
        {
            var provider = new AzureBlobStorageMultipartProvider(container, userName);
            byte[] content;
            string contentType;
            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url);
                content = response.Result.Content.ReadAsByteArrayAsync().Result;
                contentType = response.Result.Content.Headers.FirstOrDefault(x => x.Key == AvatarConstants.ContentType).Value.FirstOrDefault();
            }
            if(content.IsValidImageFormat())
                provider.UpdateBlob(content, contentType, AvatarConstants.ImageExtension);

            return provider._fileLocation;
        }

    }
}
