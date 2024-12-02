using System.Data.Entity;

namespace LabOne.Data
{
    public class PolicyContext : DbContext
    {
        public DbSet<Policy> Policies { get; set; }

        public PolicyContext() : base("name=PolicyContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configuration can be added here if needed
        }
    }
}
