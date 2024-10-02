using Microsoft.EntityFrameworkCore;
using E_Expedisi_Express.Models; // Ensure this is the correct namespace for your models

namespace E_Expedisi_Express.Data
{
    public class ExpedisiDbContext : DbContext
    {
        public ExpedisiDbContext(DbContextOptions<ExpedisiDbContext> options)
            : base(options)
        {
        }

        // Existing DbSet for MST_User
        public DbSet<MST_User> MST_User { get; set; }

        // New DbSet for DocumentType
        public DbSet<DocumentType> DocumentType { get; set; } // Add this line

        // Other DbSets can be added here as needed
    }
}
