using infraAlerta.Models;
using Microsoft.EntityFrameworkCore;

namespace infraAlerta.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Problem> Problem { get; set; }
        public DbSet<Problem_address> Problem_Address { get; set; }
        public DbSet<User_address> User_Address { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
