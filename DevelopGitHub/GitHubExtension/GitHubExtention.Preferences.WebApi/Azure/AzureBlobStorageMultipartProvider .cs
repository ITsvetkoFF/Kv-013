using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace GitHubExtention.Preferences.WebApi
{
    public class AzureBlobStorageMultipartProvider: MultipartFileStreamProvider
    {
        private CloudBlobContainer _container;
        private string _userName;
        public string _fileLocation;

        public AzureBlobStorageMultipartProvider(CloudBlobContainer container, string userName)
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
            CloudBlockBlob blob = _container.GetBlockBlobReference(_userName + extension);
            blob.Properties.ContentType = contentType;
            int length = content.Length-1;
            blob.UploadFromByteArrayAsync(content, 0, length);
            _fileLocation = blob.Uri.AbsoluteUri;
        }

        internal void DeleteBlob(string absoluteUri)
        {
            string blobName = absoluteUri.Substring(_container.Uri.AbsoluteUri.Length+1);
            CloudBlockBlob blob = _container.GetBlockBlobReference(blobName);
            blob.Delete();
        }
    }
}
