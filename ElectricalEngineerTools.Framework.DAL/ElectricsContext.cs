using ElectricalEngineerTools.Framework.DAL.Entities;
using ElectricalEngineerTools.Framework.DAL.Services;
using MySql.Data.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace ElectricalEngineerTools.Framework.DAL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class ElectricsContext : DbContext
    {
        public ElectricsContext() : base(Base.ConnectionString)
        {

        }
        public ElectricsContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }

        public DbSet<LightingFixture> LightingFixtures { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<TechnicalSpecifications> TechnicalSpecifications { get; set; }
        public DbSet<Mounting> Mountings { get; set; }
        public DbSet<ClimateApplication> ClimateApplications { get; set; }
        public DbSet<DiffuserMaterial> DiffuserMaterials { get; set; }
        public DbSet<IngressProtection> IngressProtections { get; set; }
        public DbSet<EquipmentClass> EquipmentClasses { get; set; }
        public DbSet<LightSourceInfo> LightSourceInfoes { get; set; }
        public DbSet<Dimensions> Dimensions { get; set; }
        public DbSet<Cable> Cables { get; set; }

        /*protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LightingFixture>().MapToStoredProcedures();
            modelBuilder.Entity<Cable>().MapToStoredProcedures();
        }*/
    }
}
