using Microsoft.EntityFrameworkCore;
using MSResidents.Models; // Ou o namespace onde a classe Resident está definida

namespace MSResidents.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Resident> Residents { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
