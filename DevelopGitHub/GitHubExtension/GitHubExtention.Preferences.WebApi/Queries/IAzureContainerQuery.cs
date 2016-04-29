using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtention.Preferences.WebApi.Queries
{
    public interface IAzureContainerQuery
    {
        string AbsoluteUrl { get; }

        CloudBlockBlob GetBlobReference(string blobName);
    }
}
