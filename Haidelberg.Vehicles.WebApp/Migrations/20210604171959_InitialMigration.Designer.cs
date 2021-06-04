﻿// <auto-generated />
using System;
using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Haidelberg.Vehicles.WebApp.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20210604171959_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.6")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Branches");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("DrivingLicenceCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("DrivingLicenceCategoryId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Expense", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CreatedByEmployeeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehicleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByEmployeeId");

                    b.ToTable("Expenses");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Vehicle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BranchId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastRegistrationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LicencePlate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductionYear")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("CategoryId");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Employee", b =>
                {
                    b.HasOne("Haidelberg.Vehicles.DataAccess.EF.Branch", "Branch")
                        .WithMany("Employees")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Haidelberg.Vehicles.DataAccess.EF.Category", "DrivingLicenceCategory")
                        .WithMany("Employees")
                        .HasForeignKey("DrivingLicenceCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("DrivingLicenceCategory");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Expense", b =>
                {
                    b.HasOne("Haidelberg.Vehicles.DataAccess.EF.Employee", "CreatedByEmployee")
                        .WithMany("Expenses")
                        .HasForeignKey("CreatedByEmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CreatedByEmployee");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Vehicle", b =>
                {
                    b.HasOne("Haidelberg.Vehicles.DataAccess.EF.Branch", null)
                        .WithMany("Vehicles")
                        .HasForeignKey("BranchId");

                    b.HasOne("Haidelberg.Vehicles.DataAccess.EF.Category", "Category")
                        .WithMany("Vehicles")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Branch", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Category", b =>
                {
                    b.Navigation("Employees");

                    b.Navigation("Vehicles");
                });

            modelBuilder.Entity("Haidelberg.Vehicles.DataAccess.EF.Employee", b =>
                {
                    b.Navigation("Expenses");
                });
#pragma warning restore 612, 618
        }
    }
}
