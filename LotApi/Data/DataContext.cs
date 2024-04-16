using LotApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LotApi.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // for the other conventions, we do a metadata model loop
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
                entityType.SetTableName(entityType.DisplayName());

                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Flight> Flight { get; set; }
        public DbSet<FlightLocation> FlightLocation { get; set; }
    }
}
