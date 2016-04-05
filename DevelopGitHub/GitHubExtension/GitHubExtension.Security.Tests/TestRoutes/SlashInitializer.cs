using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GitHubExtension.Security.Tests.TestRoutes
{
    public class SlashInitializer
    {
        public static string Slash { get; set; }

        public SlashInitializer()
        {
            Slash = "/";
        }
    }
}
