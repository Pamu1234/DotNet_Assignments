using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;

        public RoleRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext, IDbConnection dbConnection)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dbConnection;
        }

        public async Task<Role> CreateAsync(Role role)
        {
            _employeeManagementDataDbContext.Roles.Add(role);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return role;
        }

        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {

            var roleDataQuery = "select * from Roles";
            var result = await _dapperConnection.QueryAsync<RoleDto>(roleDataQuery);
            return result;
        }

        public async Task<Role> GetRoleDataAsync(int roleId)
        {
            var getRoleDataByIdQuery = "select * from Roles where RoleId = @roleId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<Role>(getRoleDataByIdQuery, new { roleId });
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
