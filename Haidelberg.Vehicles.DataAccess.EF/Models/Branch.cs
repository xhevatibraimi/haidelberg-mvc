using System;
using System.Collections.Generic;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class Branch
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
