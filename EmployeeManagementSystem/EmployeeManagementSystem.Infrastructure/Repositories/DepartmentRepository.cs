using Dapper;
using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Data;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
using System.Data;
using AutoMapper;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        private readonly IDbConnection _dapperConnection;
        private readonly IMapper _mapper;

        public DepartmentRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext, IDbConnection dapperConnection, IMapper mapper)
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
            var getDepartmentQuery = "select * from Departments";
            var result = await _dapperConnection.QueryAsync<DepartmentDto>(getDepartmentQuery);
            return result;
        }

        public async Task<Department> GetDepartmentAsync(int departmentId)
        {
            var getDepartmentQueryById = "select * from Departments where DepartmentId = @departmentId";
            return await _dapperConnection.QueryFirstOrDefaultAsync<Department>(getDepartmentQueryById, new { departmentId });
        }
        public async Task<Department> UpdateAsync(int departmentId, Department department)
        {
            var departmentToBeUpdatet = await GetDepartmentAsync(departmentId);
            department.UpdatedBy = 1;
            department.UpdatedDate = DateTime.UtcNow;
            department.DepartmentId = departmentId;
            department.CreatedBy = departmentToBeUpdatet.CreatedBy;
            department.CreatedDate = departmentToBeUpdatet.CreatedDate;
            _employeeManagementDataDbContext.Departments.Update(department);
            _employeeManagementDataDbContext.SaveChanges();
            return department;
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var departmentToBeDeleted = await GetDepartmentAsync(departmentId);
            _employeeManagementDataDbContext.Departments.Remove(departmentToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }

    }
}
