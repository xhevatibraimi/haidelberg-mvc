namespace Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests
{
    public class CreateRoleAssignmentRequest
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}
