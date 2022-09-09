using AutoMapper;
using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeemanagementDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IMapper _mapper;

        public DepartmentRepository(EmployeemanagementDbContext employeeManagementDataDbContext, IDbConnection dapperConnection, IMapper mapper)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
            _dapperConnection = dapperConnection;
            _mapper = mapper;
        }

        public async Task<Department> CreateAsync(Department department)
        {
            _employeeManagementDataDbContext.Departments.Add(department);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return department;

        }
        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            var getDepartmentQuery = "execute GetDepartmentList";
            var result = await _dapperConnection.QueryAsync<DepartmentDto>(getDepartmentQuery);
            return result;
        }

        public async Task<DepartmentDto> GetDepartmentAsync(int departmentId)
        {
            var getDepartmentQueryById = "select * from Departments where DepartmentId = @departmentId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<DepartmentDto>(getDepartmentQueryById, new { departmentId });
        }
        public async Task<Department> UpdateAsync(int departmentId, Department department)
        {
            var departmentToBeUpdatet = await GetDepartmentAsync(departmentId);
            department.UpdatedBy = 1;
            department.UpdatedDate = DateTime.UtcNow;
            department.DepartmentId = departmentId;
            _employeeManagementDataDbContext.Departments.Update(department);
            _employeeManagementDataDbContext.SaveChanges();
            return department;
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var departmentToBeDeleted = await _employeeManagementDataDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
           _employeeManagementDataDbContext.Departments.Remove(departmentToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }

    }
}
