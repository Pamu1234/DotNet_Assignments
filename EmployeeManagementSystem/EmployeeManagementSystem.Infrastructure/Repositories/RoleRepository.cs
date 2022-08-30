using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class RoleRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        public RoleRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            _employeeManagementDataDbContext.Roles .Add(role);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return role;
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var rolesData = await (from role in _employeeManagementDataDbContext.Roles
                                      select new RoleDto()
                                      {
                                          RoleId = role.RoleId,
                                          RoleName = role.RoleName,
                                          CreatedBy = role.CreatedBy,
                                          CreatedDate = role.CreatedDate,
                                          UpdatedBy = role.UpdatedBy,
                                          UpdatedDate = role.UpdatedDate

                                      }).ToListAsync();
            return rolesData;
        }

        public async Task<Role> GetRoleDataAsync(int roleId)
        {
            return await _employeeManagementDataDbContext.Roles.FindAsync(roleId);
        }

        public async Task<Role> UpdateAsync(int roleId, Role role)
        {
            var roleToBeUpdate = await GetRoleDataAsync(roleId);
            roleToBeUpdate.RoleId = role.RoleId;
            roleToBeUpdate.RoleName = role.RoleName;
            roleToBeUpdate.CreatedBy = role.CreatedBy;
            roleToBeUpdate.CreatedDate = role.CreatedDate;
            roleToBeUpdate.UpdatedBy = role.UpdatedBy;
            roleToBeUpdate.UpdatedDate = role.UpdatedDate;
            _employeeManagementDataDbContext.Roles.Update(roleToBeUpdate);
            _employeeManagementDataDbContext.SaveChanges();
            return roleToBeUpdate;
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var roleToBeDeleted = await GetRoleDataAsync(roleId);
            _employeeManagementDataDbContext.Roles.Remove(roleToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
