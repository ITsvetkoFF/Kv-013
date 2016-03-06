using System.ComponentModel.DataAnnotations;

namespace GitHubExtension.Identity.Models
{
        // Models used as parameters to AccountController actions.
        public class AddExternalLoginBindingModel
        {
            [Required]
            [Display(Name = "External access token")]
            public string ExternalAccessToken { get; set; }
        }

        public class RegisterExternalBindingModel
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; }
        }
}
