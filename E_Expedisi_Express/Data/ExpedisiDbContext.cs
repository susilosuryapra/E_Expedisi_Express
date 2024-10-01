using Microsoft.EntityFrameworkCore;
using E_Expedisi_Express.Models;

namespace E_Expedisi_Express.Data
{
    public class ExpedisiDbContext : DbContext
    {
        public ExpedisiDbContext(DbContextOptions<ExpedisiDbContext> options)
            : base(options)
        {
        }

        public DbSet<MST_User> MST_User { get; set; } // Menambahkan DbSet untuk MST_User
    }
}
