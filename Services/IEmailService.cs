using ResetPassword1.Models;

namespace ResetPassword1.Services
{
    public interface IEmailService
    {
        void SendEmail(User user, string operation);
    }
}