using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Security.WebApi.Models
{
    public class UserCookieModel
    {
        public string UserName { get; set; }
        public bool IsAuth { get; set; }
    }
}
