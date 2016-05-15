using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using GitHubExtention.Preferences.WebApi.Extensions;
using GitHubExtention.Preferences.WebApi.Models;

namespace GitHubExtention.Preferences.WebApi.Formatters
{
    public class FileMediaFormatter : MediaTypeFormatter
    {
        public FileMediaFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("multipart/form-data"));
        }

        public override bool CanReadType(Type type)
        {
            return type == typeof(FileModel);
        }

        public override bool CanWriteType(Type type)
        {
            return false;
        }

        public override async Task<object> ReadFromStreamAsync(
            Type type, 
            Stream readStream, 
            HttpContent content, 
            IFormatterLogger formatterLogger)
        {
            var parts = await content.ReadAsMultipartAsync();
            HttpContent fileContent = parts.GetImageContent();
            string extension = fileContent.GetFileExtension();
            string mediaType = fileContent.Headers.ContentType.MediaType;
            var imageBuffer = await fileContent.ReadAsByteArrayAsync();
            return new FileModel(mediaType, extension, imageBuffer);
        }
    }
}
