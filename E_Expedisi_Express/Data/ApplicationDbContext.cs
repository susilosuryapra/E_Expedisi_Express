using E_Expedisi_Express.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Expedisi_Express.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<DepartmentCode> DepartmentCode { get; set; }
    }
}
