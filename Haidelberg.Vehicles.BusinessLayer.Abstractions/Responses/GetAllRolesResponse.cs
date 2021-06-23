using System.Collections.Generic;

namespace Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses
{
    public class GetAllRolesResponse
    {
        public List<Role> Roles { get; set; }

        public class Role
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }
    }
}
