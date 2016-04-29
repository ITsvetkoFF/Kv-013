using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtention.Preferences.WebApi.Queries
{
    class AzureContainerQuery : IAzureContainerQuery
    {
        public AzureContainerQuery(CloudBlobContainer container)
        {
            _cloudContainer = container;
        }

        public string AbsoluteUrl
        {
            get { return _cloudContainer.Uri.AbsoluteUri; }
        }

        private readonly CloudBlobContainer _cloudContainer;

        public CloudBlockBlob GetBlobReference(string blobName)
        {
            return _cloudContainer.GetBlockBlobReference(blobName);
        }
    }
}
