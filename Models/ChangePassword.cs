using System.ComponentModel.DataAnnotations;

namespace ResetPassword1.Models
{
    public class ChangePassword
    {
        [Required]
        [EmailAddress]
        public string Email {get; set;} = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        public string Password {get; set;} = string.Empty;

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password",ErrorMessage ="Password and Confirm Password should be same")]
        public string ConfirmPassword {get; set;} = string.Empty;

        [Required]
        public string ResetToken {get; set;} = string.Empty;



    }
}