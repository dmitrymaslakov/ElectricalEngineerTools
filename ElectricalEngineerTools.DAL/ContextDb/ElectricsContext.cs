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

    }
}
