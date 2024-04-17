using Microsoft.EntityFrameworkCore;
using ResetPassword1.Models;

namespace ResetPassword1.Database
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }
        public DbSet<User> Users {get; set;}
    }
}