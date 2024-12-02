using Microsoft.EntityFrameworkCore;
using LabTwoApi.Models;

namespace LabTwoApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Record> Records { get; set; }
    }
}
