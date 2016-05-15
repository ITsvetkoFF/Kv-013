using System;
using System.Threading.Tasks;
using GitHubExtention.Preferences.WebApi.Models;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GitHubExtention.Preferences.WebApi.Queries
{
    class AzureContainerQuery : IAzureContainerQuery
    {
        private readonly CloudBlobContainer _cloudContainer;

        public AzureContainerQuery(CloudBlobContainer container)
        {
            _cloudContainer = container;
        }

        public async Task<string> UpdateBlob(FileModel file)
        {
            CloudBlockBlob blob = _cloudContainer.GetBlockBlobReference(Guid.NewGuid() + file.Extension);
            blob.Properties.ContentType = file.Type;
            int length = file.Content.Length - 1;
            await blob.UploadFromByteArrayAsync(file.Content, 0, length);
            return blob.Uri.AbsoluteUri;
        }

        public void DeleteBlob(string absoluteUri)
        {
            string blobName = absoluteUri.Substring(_cloudContainer.Uri.AbsoluteUri.Length + 1);
            CloudBlockBlob blob = _cloudContainer.GetBlockBlobReference(blobName);
            blob.Delete();
        }
    }
}
