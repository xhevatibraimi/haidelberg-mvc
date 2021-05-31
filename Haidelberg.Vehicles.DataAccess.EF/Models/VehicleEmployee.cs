using System.ComponentModel.DataAnnotations.Schema;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class VehicleEmployee
    {
        public int VehicleId { get; set; }
        public int EmployeeId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }
        
        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
