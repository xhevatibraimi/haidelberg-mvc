using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is a required field")]
        [StringLength(5, MinimumLength = 1, ErrorMessage = "Category Name should be minimum 1 and at most 5 characters long")]
        public string Name { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
    }
}
