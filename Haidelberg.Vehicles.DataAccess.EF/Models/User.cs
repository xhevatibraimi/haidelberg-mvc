using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Haidelberg.Vehicles.DataAccess.EF
{
    public class User : IdentityUser
    {
        public User() { }
        public User(string username) => UserName = username;

        public int? EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public virtual Employee Employee { get; set; }
    }
}
