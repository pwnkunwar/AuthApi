using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResetPassword1.Database;
using ResetPassword1.Models;
namespace ResetPassword1.Controllers
{
    [Route("api/[Controller]")]
    public class UserRegisterRequestController : Controller
    {
        private readonly DataContext _context;
        private readonly IEmailService _emailservice;
        public UserRegisterRequestController(DataContext context, IEmailService emailService)
        {
            _context = context;
            _emailservice = emailService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> UserRegister(UserRegisterRequest request)
        {
            if (ModelState.IsValid)
            {

                if (_context.Users.Any(u => u.Email == request.Email))
                {
                    return BadRequest("User already exits");
                }


                GeneratePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);


                var user = new User
                {
                    Guid = Guid.NewGuid(),
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    VerificationToken = GenerateRandomToken()


                };

                _context.Users.Add(user);
                SendEmail(user, "verification");
                await _context.SaveChangesAsync();
                

                return Ok("User created successfully");



            }
            return BadRequest("Please use correct entries");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin request)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
                if (user == null)
                {
                    return BadRequest("User or Password Incorrect");
                }
                if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("User or Password Incorrect");
                }
                if (user.VerifiedAt == null)
                {
                    return BadRequest("First Verified Email");
                }
                return Ok($"Welcome Back {request.Email}");
            }
            return BadRequest("Please use correct entries");
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> Verify(Verify verify)
        {
            var token = await _context.Users.FirstOrDefaultAsync(u=>u.VerificationToken == verify.Token);
            if(token == null)
            {
                return BadRequest("Token is Invalid");
            }
            token.VerifiedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok("Verified");
        }
       
       

        [HttpPost("forgot-password")]
        public async Task<IActionResult> forgotPassword(forgotPassword request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.Email == request.Email);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            if(user.VerifiedAt == null)
            {
                SendEmail(user, "verification");
                return BadRequest("User is not Verified! First Verified");
            }
            string passwordResetToken = GenerateRandomToken();
            user.PasswordResetToken = passwordResetToken;
            user.PasswordResetTokenExpires = DateTime.Now.AddDays(1);
            await _context.SaveChangesAsync();
            SendEmail(user, "passwordreset");
            return Ok("Password reset link is sent");

        }

        [HttpPost("change-password")]
        public async Task<IActionResult> changePassword(ChangePassword request)
        {
            if(ModelState.IsValid)
            {
            var user = await _context.Users.FirstOrDefaultAsync(u=> u.Email == request.Email);
            if(user == null)
            {
                return BadRequest("User not found");
            }
            if(user.VerifiedAt == null)
            {
                return BadRequest("First Verify the User");
            }
            if(user.PasswordResetTokenExpires < DateTime.UtcNow)
            {
                user.PasswordResetToken = GenerateRandomToken();
                await _context.SaveChangesAsync();
                SendEmail(user,"verifiaction" );
                return BadRequest("Token is expired! Please Check for new token in you mail box");

            }
            if(user.PasswordResetToken == request.ResetToken)
            {
                GeneratePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.PasswordResetToken= null;
                user.PasswordResetTokenExpires = null;
                await _context.SaveChangesAsync();
                return Ok("Password change successfully");


            }
            }
            return BadRequest();



        }



        private void GeneratePasswordHash(string passoword, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passoword));
            }

        }
        private string GenerateRandomToken()
        {
            return Guid.NewGuid().ToString("D");
        }
        private bool VerifyPasswordHash(string passoword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA256(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passoword));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
         void SendEmail(User user, string operation)
        {

            _emailservice.SendEmail(user, operation);
 
        }

    }
}