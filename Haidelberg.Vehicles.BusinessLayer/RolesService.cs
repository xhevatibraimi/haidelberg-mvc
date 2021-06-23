using Haidelberg.Vehicles.BusinessLayer.Abstractions;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Requests;
using Haidelberg.Vehicles.BusinessLayer.Abstractions.Responses;
using Haidelberg.Vehicles.DataAccess.EF;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Haidelberg.Vehicles.BusinessLayer
{
    public class RolesService : IRolesService
    {
        private readonly DatabaseContext _context;
        private readonly RoleManager<IdentityRole<string>> _roleManager;
        private readonly UserManager<User> _userManager;

        public RolesService(DatabaseContext context, RoleManager<IdentityRole<string>> roleManager, UserManager<User> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<ServiceResult> TryCreateRole(CreateRoleRequest request)
        {
            var response = new ServiceResult();
            var role = new IdentityRole<string>(request.Name);
            role.Id = Guid.NewGuid().ToString();
            var identityResult = await _roleManager.CreateAsync(role);
            if (!identityResult.Succeeded)
            {
                response.Errors = identityResult.Errors.Select(x => x.Description).ToList();
                return response;
            }

            response.IsSuccessfull = true;
            return response;
        }

        public async Task<ServiceResult> TryCreateRoleAssignment(CreateRoleAssignmentRequest request)
        {
            var response = new ServiceResult();
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                response.AddError("user not found");
                return response;
            }

            var identityResult = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (!identityResult.Succeeded)
            {
                response.Errors = identityResult.Errors.Select(x => x.Description).ToList();
                return response;
            }

            response.IsSuccessfull = true;
            return response;
        }

        public async Task<ServiceResult> TryDeleteRole(string id)
        {
            var response = new ServiceResult();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                response.AddError("role not found");
                return response;
            }

            var identityResult = await _roleManager.DeleteAsync(role);
            if (!identityResult.Succeeded)
            {
                response.Errors = identityResult.Errors.Select(x => x.Description).ToList();
                return response;
            }

            response.IsSuccessfull = true;
            return response;
        }

        public ServiceResult<GetAllRolesResponse> TryGetAllRoles()
        {
            var roles = _context.Roles.Select(x => new GetAllRolesResponse.Role
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();

            return new ServiceResult<GetAllRolesResponse>
            {
                IsSuccessfull = true,
                Result = new GetAllRolesResponse
                {
                    Roles = roles
                }
            };
        }

        public ServiceResult<GetCreateRoleAssignmentScreenVM> TryGetCreateRoleAssignmentScreenVM()
        {
            var result = new ServiceResult<GetCreateRoleAssignmentScreenVM>();
            result.Result = new GetCreateRoleAssignmentScreenVM();
            result.Result.Users = _context.Users.Select(x => new GetCreateRoleAssignmentScreenVM.User
            {
                Id = x.Id,
                Username = x.UserName
            }).ToList();
            result.Result.Roles = _context.Roles.Select(x => new GetCreateRoleAssignmentScreenVM.Role
            {
                Name = x.Name
            }).ToList();
            result.IsSuccessfull = true;
            return result;
        }
    }
}
