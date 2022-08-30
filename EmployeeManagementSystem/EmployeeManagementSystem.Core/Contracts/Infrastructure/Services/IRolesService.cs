using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Core.Contracts.Infrastructure.Services
{
    public interface IRolesService
    {
        Task<Role> CreateAsync(Role role);
        Task DeleteRoleAsync(int roleId);
        Task<Role> GetRoleDataAsync(int roleId);
        Task<IEnumerable<RoleDto>> GetRolesAsync();
        Task<Role> UpdateAsync(int roleId, Role role);
    }
}
