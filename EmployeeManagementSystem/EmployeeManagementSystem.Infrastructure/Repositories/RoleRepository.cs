using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;

        public RoleRepository(EmployeemanagementDbContext employeeManagementDataDbContext, IDbConnection dbConnection)
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
            role.RoleId = role.RoleId;
            role.RoleName = role.RoleName;
            role.CreatedBy = role.CreatedBy;
            role.CreatedDate = role.CreatedDate;
            role.UpdatedBy = role.UpdatedBy;
            role.UpdatedDate = role.UpdatedDate;
            _employeeManagementDataDbContext.Roles.Update(role);
            _employeeManagementDataDbContext.SaveChanges();
            return role;
        }

        public async Task DeleteRoleAsync(int roleId)
        {
            var roleToBeDeleted = await GetRoleDataAsync(roleId);
            _employeeManagementDataDbContext.Roles.Remove(roleToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }
    }
}
