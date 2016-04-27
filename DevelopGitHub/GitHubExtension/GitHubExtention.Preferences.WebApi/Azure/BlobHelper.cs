using GitHubExtention.Preferences.WebApi.Constants;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace GitHubExtention.Preferences.WebApi
{
    internal static class BlobHelper
    {
        public static CloudBlobContainer GetWebApiContainer()
        {
            string str =
                CloudConfigurationManager.GetSetting(AvatarConstants.StorageConnectionString);
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(str);

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(AvatarConstants.AzureContainer);
            container.CreateIfNotExists();

            var permissions = container.GetPermissions();
            if (permissions.PublicAccess == BlobContainerPublicAccessType.Off)
            {
                permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                container.SetPermissions(permissions);
            }

            return container;
        }
    }
}
