using Microsoft.EntityFrameworkCore;

namespace Mepas.Sub.API.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {
        }

        public DbSet<Subscriber> Subscribers { get; set; }
    }
}
