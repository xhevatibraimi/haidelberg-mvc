using System.ComponentModel.DataAnnotations.Schema;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public int VehicleId { get; set; }
        public int CreatedByEmployeeId { get; set; }

        [ForeignKey("CreatedByEmployeeId")]
        public virtual Employee CreatedByEmployee { get; set; }
    }
}
