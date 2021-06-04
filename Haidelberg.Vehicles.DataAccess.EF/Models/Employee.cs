using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.DataAccess.EF
{
   public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public int DrivingLicenceCategoryId { get; set; }
        public int BranchId { get; set; }

        [ForeignKey("DrivingLicenceCategoryId")]
        public virtual Category DrivingLicenceCategory { get; set; }

        [ForeignKey("BranchId")]
        public virtual Branch Branch { get; set; }

        public virtual ICollection<Expense> Expenses { get; set; }
        //public virtual ICollection<VehicleEmployee> VehicleEmployees { get; set; }
    }
}
