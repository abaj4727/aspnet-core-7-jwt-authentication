using JWTImplimentation.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTImplimentation.Context
{
    public class JWTContext:DbContext
    {
        public JWTContext(DbContextOptions<JWTContext> options):base(options)
        {
            
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Employee> employees { get; set; }
    }
}
