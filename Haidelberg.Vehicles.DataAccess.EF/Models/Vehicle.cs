using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Production Year is a required field!")]
        [Range(2000, 2100, ErrorMessage = "The value should be between 2000 and 2100")]
        public int ProductionYear { get; set; }
        [Required]
        public DateTime LastRegistrationDate { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicencePlate { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //public virtual ICollection<VehicleEmployee> VehicleEmployees { get; set; }

    }
}
