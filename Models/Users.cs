using System.ComponentModel.DataAnnotations;

namespace ResetPassword1.Models
{
    public class User
    {
        [Key]
        public Guid Guid {get; set;}
        public string Email {get; set;} = string.Empty;
        public byte[] PasswordHash {get; set;} = new byte[32];

        public byte[] PasswordSalt {get; set;} = new byte[32];

        public string VerificationToken {get; set;} = string.Empty;

        public DateTime? VerifiedAt {get; set;}

        public string PasswordResetToken {get; set;} = string.Empty;
        public DateTime? PasswordResetTokenExpires {get; set;}


    }
}