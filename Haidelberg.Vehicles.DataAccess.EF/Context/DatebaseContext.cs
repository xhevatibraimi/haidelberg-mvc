using Microsoft.EntityFrameworkCore;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class DatebaseContext : DbContext
    {
        public DatebaseContext(DbContextOptions<DatebaseContext> options): base(options)
        {
        }

        public DbSet<Branch> Branches { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleEmployee> VehicleEmployees { get; set; }
    }
}
