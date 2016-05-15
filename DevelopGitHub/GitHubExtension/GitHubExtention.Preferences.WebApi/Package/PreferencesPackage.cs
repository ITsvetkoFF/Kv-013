using GitHubExtension.Preferences.DAL.Model;
using GitHubExtention.Preferences.WebApi.Constants;
using GitHubExtention.Preferences.WebApi.Queries;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace GitHubExtention.Preferences.WebApi.Package
{
    public class PreferencesPackage : IPackage
    {
        public void RegisterServices(Container container)
        {
            container.Register<CloudBlobContainer>(
                () =>
            {
                string str = CloudConfigurationManager.GetSetting(AvatarConstants.StorageConnectionString);
                var account = CloudStorageAccount.Parse(str);
                var client = account.CreateCloudBlobClient();
                var azureContainer = client.GetContainerReference(AvatarConstants.AzureContainer);
                azureContainer.CreateIfNotExists();
                var permissions = azureContainer.GetPermissions();
                if (permissions.PublicAccess == BlobContainerPublicAccessType.Off)
                {
                    permissions.PublicAccess = BlobContainerPublicAccessType.Blob;
                    azureContainer.SetPermissions(permissions);
                }

                return azureContainer;
            },
            Lifestyle.Singleton);
            container.Register<IAzureContainerQuery, AzureContainerQuery>();
            container.Register<PreferencesContext>(Lifestyle.Scoped);
            container.Register<IPreferencesQuery, PreferencesQuery>(Lifestyle.Scoped);
        }
    }
}
