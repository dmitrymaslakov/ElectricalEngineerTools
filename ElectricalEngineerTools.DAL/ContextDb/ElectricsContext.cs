using ElectricalEngineerTools.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace ElectricalEngineerTools.DAL.ContextDb
{
    public class ElectricsContext : DbContext
    {
        public DbSet<LightingFixture> LightingFixtures { get; set; }
        public DbSet<Cable> Cables { get; set; }

        public ElectricsContext(DbContextOptions<ElectricsContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            /*modelBuilder.Entity<LightingFixture>(entity =>
                        {
                            entity.HasOne(l => l.Cable)
                            .WithMany(c => c.LightingFixtures)
                            .HasForeignKey(l => l.CableId)
                            .OnDelete(DeleteBehavior.SetNull);
                        });*/
        }
    }
}
