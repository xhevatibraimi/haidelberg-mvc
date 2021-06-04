﻿using Microsoft.EntityFrameworkCore;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options): base(options)
        {
            //base.Database.EnsureCreated();
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        //public DbSet<VehicleEmployee> VehicleEmployees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<VehicleEmployee>()
            //    .HasKey(x => new
            //    {
            //        x.EmployeeId,
            //        x.VehicleId
            //    });

            //modelBuilder.Entity<VehicleEmployee>()
            //    .HasOne(ve => ve.Employee)
            //    .WithMany(e => e.VehicleEmployees)
            //    .HasForeignKey(ve => ve.EmployeeId);

            //modelBuilder.Entity<VehicleEmployee>()
            //    .HasOne(ve => ve.Vehicle)
            //    .WithMany(v => v.VehicleEmployees)
            //    .HasForeignKey(ve => ve.VehicleId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
