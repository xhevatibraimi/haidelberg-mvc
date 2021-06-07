﻿using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class Vehicle
    {
        public int Id { get; set; }
        //[Required(ErrorMessage = "Production Year is a required field!")]
        //[Display(Name = "Production Year")]
        public int ProductionYear { get; set; }
        public DateTime LastRegistrationDate { get; set; }
        public string Model { get; set; }
        public string LicencePlate { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        //public virtual ICollection<VehicleEmployee> VehicleEmployees { get; set; }

    }
}
