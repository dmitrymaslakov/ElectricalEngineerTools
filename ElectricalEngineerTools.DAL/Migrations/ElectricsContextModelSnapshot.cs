﻿// <auto-generated />
using System;
using ElectricalEngineerTools.DAL.ContextDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ElectricalEngineerTools.DAL.Migrations
{
    [DbContext(typeof(ElectricsContext))]
    partial class ElectricsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ElectricalEngineerTools.DAL.Entities.Cable", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("CoresNumber")
                        .HasColumnType("int");

                    b.Property<double>("Section")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("Cables");
                });

            modelBuilder.Entity("ElectricalEngineerTools.DAL.Entities.LightingFixture", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("char(36)");

                    b.Property<string>("Brand")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("CableId")
                        .HasColumnType("char(36)");

                    b.Property<string>("ClimaticModification")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("DiffuserMaterial")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("IP")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<bool>("IsFireproof")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LdtIesFile")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Manufacturer")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<double>("Power")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CableId");

                    b.ToTable("LightingFixtures");
                });

            modelBuilder.Entity("ElectricalEngineerTools.DAL.Entities.LightingFixture", b =>
                {
                    b.HasOne("ElectricalEngineerTools.DAL.Entities.Cable", "Cable")
                        .WithMany("LightingFixtures")
                        .HasForeignKey("CableId");
                });
#pragma warning restore 612, 618
        }
    }
}
