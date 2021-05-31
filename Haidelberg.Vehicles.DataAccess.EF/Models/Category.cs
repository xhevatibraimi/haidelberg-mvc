using System.Collections.Generic;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
