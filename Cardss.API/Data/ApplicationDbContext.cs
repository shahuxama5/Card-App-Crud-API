using Cardss.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cardss.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<Card> Cards { get; set; }
    }
}
