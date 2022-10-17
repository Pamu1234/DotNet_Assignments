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
            await _employeeManagementDataDbContext.Departments.AddAsync(department);
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

        public async Task<DepartmentDto> GetDepartmentByNameAsync(string departmentName)
        { 
            var getDepartmentQueryById = "select * from Departments where DepartmentName = @departmentName";
            return await _dapperConnection.QueryFirstOrDefaultAsync<DepartmentDto>(getDepartmentQueryById, new { departmentName });
        }
        public async Task<Department> GetDepartmentById(int deptId)
        {
            var departmentData= "select * from Departments where DepartmentId = @deptId";
            var result = await _dapperConnection.QueryFirstOrDefaultAsync<Department>(departmentData, new { deptId });
            return result;
        }

        public async Task<Department> UpdateAsync(Department updatedDepartment)
        {            
             _employeeManagementDataDbContext.Departments.Update(updatedDepartment);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return updatedDepartment;
        }

        public async Task<IEnumerable<EmployeeDto>> GetListOfEmpWorkingInEachDept(int deptId)
        {
            var empCount =await (from emp in _employeeManagementDataDbContext.Employees
                                 join dept in _employeeManagementDataDbContext.Departments
                                 on emp.DepartmentId equals dept.DepartmentId
                                 join Role in _employeeManagementDataDbContext.Roles
                                 on emp.RoleId equals Role.RoleId
                                 where emp.DepartmentId == deptId
                           select new EmployeeDto
                           {
                               EmployeeId = emp.EmployeeId,
                               Address = emp.Address,
                               FirstName = emp.FirstName,
                               LastName = emp.LastName,
                               EmailId = emp.EmailId,
                               Contact=emp.Contact,
                               Salary=emp.Salary,
                               DepartmentName = dept.DepartmentName,
                               RoleName = Role.RoleName

                           }).ToListAsync();
            return empCount;
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var departmentToBeDeleted = await _employeeManagementDataDbContext.Departments.FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
           _employeeManagementDataDbContext.Departments.Remove(departmentToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }


    }
}
