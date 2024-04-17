using System.ComponentModel.DataAnnotations;

namespace ResetPassword1.Models
{
    public class Verify
    {
        [Required]
        public string Token {get; set;} = string.Empty;
    }
}