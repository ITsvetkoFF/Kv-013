using GitHubExtention.Preferences.WebApi.Queries;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubExtention.Preferences.WebApi
{
    public class AzureBlobStorageMultipartProvider: MultipartFileStreamProvider
    {
        private IAzureContainerQuery _container;
        private string _userName;
        public string _fileLocation;

        public AzureBlobStorageMultipartProvider(IAzureContainerQuery container, string userName)
            : base(Path.GetTempPath())
        {
            _container = container;
            _userName = userName;
        }

        public override Task ExecutePostProcessingAsync()
        {
            foreach (var fileData in this.FileData)
            {
                byte[] content = File.ReadAllBytes(fileData.LocalFileName);
                if (content.IsValidImageFormat())
                    UpdateBlob(content, fileData.Headers.ContentType.MediaType, Path.GetExtension(fileData.Headers.ContentDisposition.FileName.Trim('"')));
            }

            return base.ExecutePostProcessingAsync();
        }

        public void UpdateBlob(byte[] content, string contentType, string extension)
        {
            CloudBlockBlob blob = _container.GetBlobReference(_userName + extension);
            blob.Properties.ContentType = contentType;
            int length = content.Length-1;
            blob.UploadFromByteArrayAsync(content, 0, length);
            _fileLocation = blob.Uri.AbsoluteUri;
        }

        internal void DeleteBlob(string absoluteUri)
        {
            string blobName = absoluteUri.Substring(_container.AbsoluteUrl.Length + 1);
            CloudBlockBlob blob = _container.GetBlobReference(blobName);
            blob.Delete();
        }
    }
}
