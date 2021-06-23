using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.BusinessLayer.Abstractions
{
    public interface IRolesService
    {
        ServiceResult<GetAllRolesResponse> TryGetAllRoles();
        Task<ServiceResult> TryCreateRole(CreateRoleRequest request);
        Task<ServiceResult> TryDeleteRole(string id);
        ServiceResult<GetCreateRoleAssignmentScreenVM> TryGetCreateRoleAssignmentScreenVM();
        Task<ServiceResult> TryCreateRoleAssignment(CreateRoleAssignmentRequest request);
    }
}
