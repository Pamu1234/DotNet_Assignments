using EmployeeManagementSystem.Core.Dtos;
using EmployeeManagementSystem.Core.Entities;
using EmployeeManagementSystem.Infrastructure.Models;
using EmployeeManagementSystem.Infrastructure.Repositories.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Infrastructure.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly EmployeeManagementDataDbContext _employeeManagementDataDbContext;
        public DepartmentRepository(EmployeeManagementDataDbContext employeeManagementDataDbContext)
        {
            _employeeManagementDataDbContext = employeeManagementDataDbContext;
        }

        public async Task<Department> CreateAsync(Department department)
        {
            _employeeManagementDataDbContext.Departments.Add(department);
            await _employeeManagementDataDbContext.SaveChangesAsync();
            return department;

        }

        public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync()
        {
            var employeeList = await (from department in _employeeManagementDataDbContext.Departments
                                      select new DepartmentDto()
                                      {
                                          DepartmentId = department.DepartmentId,
                                          DepartmentName = department.DepartmentName,
                                          Description = department.Description,
                                          CreatedBy = department.CreatedBy,
                                          CreatedDate = department.CreatedDate,
                                          UpdatedBy = department.UpdatedBy,
                                          UpdatedDate = department.UpdatedDate,

                                      }).ToListAsync();
            return employeeList;
        }

        public async Task<Department> GetDepartmentAsync(int departmentId)
        {
            return await _employeeManagementDataDbContext.Departments.FindAsync(departmentId);
        }
        public async Task<Department> UpdateAsync(int departmentId, Department department)
        {
            var departmentToBeUpdate = await GetDepartmentAsync(departmentId);
            departmentToBeUpdate.DepartmentId = department.DepartmentId;
            departmentToBeUpdate.DepartmentName = department.DepartmentName;
            departmentToBeUpdate.Description = department.Description;
            departmentToBeUpdate.CreatedBy = department.CreatedBy;
            departmentToBeUpdate.CreatedDate = department.CreatedDate;
            departmentToBeUpdate.UpdatedBy = department.UpdatedBy;
            departmentToBeUpdate.UpdatedDate = department.UpdatedDate;
            _employeeManagementDataDbContext.Departments.Update(departmentToBeUpdate);
            _employeeManagementDataDbContext.SaveChanges();
            return departmentToBeUpdate;
        }

        public async Task DeleteDepartmentAsync(int departmentId)
        {
            var departmentToBeDeleted = await GetDepartmentAsync(departmentId);
            _employeeManagementDataDbContext.Departments.Remove(departmentToBeDeleted);
            await _employeeManagementDataDbContext.SaveChangesAsync();
        }

    }
}
