using System.Linq;
using System.Text;
using GitHubExtention.Preferences.WebApi.Models;

namespace GitHubExtention.Preferences.WebApi.Extensions
{
    public static class ImageValidation
    {
        public static bool IsValidImageFormat(this FileModel imageCFile)
        {
            foreach (var pattern in new byte[][] 
            {
                    Encoding.ASCII.GetBytes("BM"),
                    Encoding.ASCII.GetBytes("GIF"),
                    new byte[] { 137, 80, 78, 71 },     // PNG
                    new byte[] { 73, 73, 42 },          // TIFF
                    new byte[] { 77, 77, 42 },          // TIFF
                    new byte[] { 255, 216, 255, 224 },  // jpeg
                    new byte[] { 255, 216, 255, 225 },  // jpeg canon
                    new byte[] { 255, 216, 255, 219 }   // jpeg2
            })
            {
                if (pattern.SequenceEqual(imageCFile.Content.Take(pattern.Length)))
                {
                    return true;
                }    
            }

            return false;
        }
    }
}
