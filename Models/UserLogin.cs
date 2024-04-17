using System.ComponentModel.DataAnnotations;

namespace ResetPassword1.Models
{
   public class UserLogin
    {
        [Required]
        [EmailAddress]
        public string Email {set; get;} = string.Empty;

        [Required]
        public string Password {get; set;} = string.Empty;
    }
}