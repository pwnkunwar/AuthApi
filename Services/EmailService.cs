using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using ResetPassword1.Models;

namespace ResetPassword1.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }
        public void SendEmail(User user, string operation)
        { 
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("@gmail.com"));
            email.To.Add(MailboxAddress.Parse(user.Email));
          switch (operation.ToLowerInvariant())
    {
        case "verification":
            email.Subject = "Password Verification";
            email.Body = new TextPart(TextFormat.Html) { Text = $"Click here to verify your account -> http://localhost:5202/api/UserRegisterRequest/Verify?Token={user.VerificationToken}" };
            break;

        case "passwordreset":
            email.Subject = "Password Reset";
            email.Body = new TextPart(TextFormat.Html) { Text = $"Click here to reset your account password -> http://localhost:5202/api/UserRegisterRequest/forgot-password?Token={user.PasswordResetToken}" };
            break;

        default:
            throw new ArgumentException("Invalid operation type", nameof(operation));
    }
            using ( var smtp = new SmtpClient())
            {

            var smtpSettings = _config.GetSection("SmtpSettings");
            smtp.Connect(smtpSettings["Host"], 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(smtpSettings["User"], smtpSettings["Password"]);
            smtp.Send(email);
            smtp.Disconnect(true);
            }


            



        }
    }
}