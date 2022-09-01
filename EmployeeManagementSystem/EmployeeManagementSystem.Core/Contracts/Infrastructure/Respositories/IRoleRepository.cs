using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public interface IRoleRepository
    {
        Task<Role> CreateAsync(Role role);
        Task DeleteRoleAsync(int roleId);
        Task<Role> GetRoleDataAsync(int roleId);
        Task<IEnumerable<RoleDto>> GetRolesAsync();
        Task<Role> UpdateAsync(int roleId, Role role);
    }
}