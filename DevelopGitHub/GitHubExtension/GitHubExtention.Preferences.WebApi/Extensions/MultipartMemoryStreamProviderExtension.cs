using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace GitHubExtention.Preferences.WebApi.Extensions
{
    public static class MultipartMemoryStreamProviderExtension
    {
        public static HttpContent GetImageContent(this MultipartMemoryStreamProvider provider) 
        {
            IEnumerable<MediaTypeHeaderValue> allowedMediaTypes = new List<MediaTypeHeaderValue>
            {
                new MediaTypeHeaderValue("image/gif"),
                new MediaTypeHeaderValue("image/jpeg"),
                new MediaTypeHeaderValue("image/pjpeg"),
                new MediaTypeHeaderValue("image/png"),
                new MediaTypeHeaderValue("image/tiff")
            };
            return provider.Contents.FirstOrDefault(x =>
                allowedMediaTypes.Contains(x.Headers.ContentType));
        }
    }
}
