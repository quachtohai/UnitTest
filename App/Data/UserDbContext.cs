using App.Models;
using Microsoft.EntityFrameworkCore;

namespace App.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<User> Users { set; get; }
    }
}
