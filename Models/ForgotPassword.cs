using System.ComponentModel.DataAnnotations;

namespace ResetPassword1.Models
{
    public class forgotPassword
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;} = string.Empty;
    }
}