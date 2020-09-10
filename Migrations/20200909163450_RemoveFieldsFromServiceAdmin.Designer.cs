﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ServiceManagement.Database;

namespace ServiceManagement.Migrations
{
    [DbContext(typeof(WorkshopContext))]
    [Migration("20200909163450_RemoveFieldsFromServiceAdmin")]
    partial class RemoveFieldsFromServiceAdmin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ServiceManagement.Models.Registration", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateOfRepair")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateRegistered")
                        .HasColumnType("datetime2");

                    b.Property<string>("VehicleRegistrationNumber")
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("ID");

                    b.HasIndex("VehicleRegistrationNumber");

                    b.ToTable("Registration");
                });

            modelBuilder.Entity("ServiceManagement.Models.Repair", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("MechanicID")
                        .HasColumnType("int");

                    b.Property<int>("VehicleID")
                        .HasColumnType("int");

                    b.Property<string>("VehicleRegistrationNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("ID");

                    b.HasIndex("MechanicID");

                    b.HasIndex("VehicleRegistrationNumber");

                    b.ToTable("Repair");
                });

            modelBuilder.Entity("ServiceManagement.Models.Service", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("RepairID")
                        .HasColumnType("int");

                    b.Property<int>("RepairTimeInHours")
                        .HasColumnType("int");

                    b.Property<int>("WorkshopID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("RepairID");

                    b.HasIndex("WorkshopID");

                    b.ToTable("Service");
                });

            modelBuilder.Entity("ServiceManagement.Models.ServiceAdmin", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.Property<int>("RepairTimeInHours")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("ServiceAdmin");
                });

            modelBuilder.Entity("ServiceManagement.Models.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.ToTable("User");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("ServiceManagement.Models.Vehicle", b =>
                {
                    b.Property<string>("RegistrationNumber")
                        .HasColumnType("nvarchar(6)")
                        .HasMaxLength(6);

                    b.Property<float>("EngineCapacity")
                        .HasColumnType("real");

                    b.Property<int>("FuelType")
                        .HasColumnType("int");

                    b.Property<string>("Make")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<DateTime>("ManufactureDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("RegistrationNumber");

                    b.ToTable("Vehicle");
                });

            modelBuilder.Entity("ServiceManagement.Models.Workshop", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("BuildingNumber")
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<int>("RegistrationID")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("ID");

                    b.HasIndex("RegistrationID")
                        .IsUnique();

                    b.ToTable("Workshop");
                });

            modelBuilder.Entity("ServiceManagement.Models.Mechanic", b =>
                {
                    b.HasBaseType("ServiceManagement.Models.User");

                    b.Property<double>("Salary")
                        .HasColumnType("float");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Mechanic");
                });

            modelBuilder.Entity("ServiceManagement.Models.Registration", b =>
                {
                    b.HasOne("ServiceManagement.Models.Vehicle", "Vehicle")
                        .WithMany()
                        .HasForeignKey("VehicleRegistrationNumber");
                });

            modelBuilder.Entity("ServiceManagement.Models.Repair", b =>
                {
                    b.HasOne("ServiceManagement.Models.Mechanic", "Mechanic")
                        .WithMany()
                        .HasForeignKey("MechanicID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceManagement.Models.Vehicle", "Vehicle")
                        .WithMany("Repairs")
                        .HasForeignKey("VehicleRegistrationNumber")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceManagement.Models.Service", b =>
                {
                    b.HasOne("ServiceManagement.Models.Repair", "Repair")
                        .WithMany("Services")
                        .HasForeignKey("RepairID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ServiceManagement.Models.Workshop", "Workshop")
                        .WithMany("Services")
                        .HasForeignKey("WorkshopID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ServiceManagement.Models.Workshop", b =>
                {
                    b.HasOne("ServiceManagement.Models.Registration", "Registration")
                        .WithOne("Workshop")
                        .HasForeignKey("ServiceManagement.Models.Workshop", "RegistrationID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
