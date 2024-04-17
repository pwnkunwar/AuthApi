using System.ComponentModel.DataAnnotations;

namespace ResetPassword1.Models
{
    public class UserRegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;} = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;} = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="Password and ConfirmPassword should be same!")]
        public string ConfirmPassword {get; set;} = string.Empty;
         
    }
}