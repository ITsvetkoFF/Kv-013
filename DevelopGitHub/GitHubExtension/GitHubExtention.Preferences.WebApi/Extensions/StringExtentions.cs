using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using GitHubExtention.Preferences.WebApi.Constants; 

namespace GitHubExtention.Preferences.WebApi
{
    public static class StringExtentions
    {
        public static string SaveAvatarToBlobStorage(this string url,  string userName)
        {
            var provider = new AzureBlobStorageMultipartProvider(BlobHelper.GetWebApiContainer(), userName);
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
