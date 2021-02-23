using ElectricalEngineerTools.Framework.DAL.Entities;
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

        public DbSet<LightingFixture> LightingFixtures { get; set; }
        public DbSet<Cable> Cables { get; set; }


        public ElectricsContext() : base("DefaultConnection")
        {

        }
        public ElectricsContext(DbConnection existingConnection, bool contextOwnsConnection)
            : base(existingConnection, contextOwnsConnection)
        {

        }
    }

    public class Example
    {
        public static void ExecuteExample()
        {
            try
            {
                using (var context = new ElectricsContext())
                {
                    var l = context.LightingFixtures.ToList();
                }
            }
            catch (Exception e)
            {
                throw;
            }
            /*string _connectionString = "server=localhost;user=root;password=HichnikMySQL1985;database=electrics";

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                var transaction = connection.BeginTransaction();

                try
                {
                    using (var context = new ElectricsContext(connection, false))
                    {
                        context.Database.UseTransaction(transaction);
                        var lightings = context.Set<LightingFixture>().ToList();
                        var lightings1 = context.Set<LightingFixture>().Local;
                    }
                    transaction.Commit();
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    throw;
                }
            }*/
        }
        public static void Add(ElectricsContext context)
        {
            var lightings = new List<LightingFixture>
            {
                new LightingFixture
                {
                    BPSU = false,
                    Brand = "ЛПО12-910-4х36",
                    ClimaticModification = "УХЛ5",
                    Description = "Светильник потолочный с люминесцентной лампой",
                    DiffuserMaterial = "Опаловый рассеиватель",
                    DwgFile = "dwg.dwg",
                    IP = "IP54",
                    IsFireproof = true,
                    LdtIesFile = "ldt.ldt",
                    Length = "1300",
                    LightSourceType = "T8 G13",
                    Manufacturer = "ООО СВС Лайтинг",
                    Power = 144.0,
                    Width = "600"
                }
             };
            context.LightingFixtures.AddRange(lightings);
            context.SaveChanges();
        }
    }
}
