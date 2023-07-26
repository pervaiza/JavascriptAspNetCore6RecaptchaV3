using System.ComponentModel.DataAnnotations;

namespace JavascriptAspNetCore6RecaptchaV3.Models
{
    public class NewUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        //Recaptcha
        public string RecaptchaToken { get;set; }
    }
}
