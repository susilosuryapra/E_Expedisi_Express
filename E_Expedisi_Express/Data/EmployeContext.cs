using E_Expedisi_Express.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Expedisi_Express.Data
{
    public class E_Expedisi_ExpressContext : DbContext
    {
        public E_Expedisi_ExpressContext(DbContextOptions<E_Expedisi_ExpressContext> options) : base(options) { }

        public DbSet<MST_Asset> MST_Asset { get; set; }

        public DbSet<MST_User> MST_User { get; set; }

    }
}