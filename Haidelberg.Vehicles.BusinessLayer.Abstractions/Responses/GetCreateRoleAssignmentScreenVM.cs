using System.Collections.Generic;

namespace Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses
{
    public class GetCreateRoleAssignmentScreenVM
    {
        public List<User> Users { get; set; }
        public List<Role> Roles { get; set; }

        public class User
        {
            public string Id { get; set; }
            public string Username { get; set; }
        }

        public class Role
        {
            public string Name { get; set; }
        }
    }
}
