namespace GitHubExtention.Preferences.WebApi.Models
{
    public class FileModel
    {
        public FileModel(string contentType, string fileExtension, byte[] fileContent)
        {
            Type = contentType;
            Extension = fileExtension;
            Content = fileContent;
        }

        public string Type { get; set; }

        public string Extension { get; set; }

        public byte[] Content { get; set; }
    }
}
