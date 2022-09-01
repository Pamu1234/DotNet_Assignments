using EmployeeManagementSystem.Core.Contracts.Infrastructure.Services;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories;

namespace EmployeeManagementSystem.Infrastructure.Services
{
    public class RoleService : IRolesService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public Task<Role> CreateAsync(Role role)
        {
            return _roleRepository.CreateAsync(role);
        }

        public Task DeleteRoleAsync(int roleId)
        {
            return _roleRepository.DeleteRoleAsync(roleId);
        }

        public Task<Role> GetRoleDataAsync(int roleId)
        {
            return _roleRepository.GetRoleDataAsync(roleId);
        }

        public Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            return _roleRepository.GetRolesAsync();
        }

        public Task<Role> UpdateAsync(int roleId, Role role)
        {
            return _roleRepository.UpdateAsync(roleId, role);
        }
    }
}
