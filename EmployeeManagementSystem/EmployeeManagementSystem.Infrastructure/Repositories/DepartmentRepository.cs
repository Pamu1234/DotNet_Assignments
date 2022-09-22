﻿using AutoMapper;
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

        public async Task<DepartmentDto> GetDepartmentByNameAsync(string departmentName)
        { 
            var getDepartmentQueryById = "select * from Departments where DepartmentName = @departmentName";
            return await _dapperConnection.QueryFirstOrDefaultAsync<DepartmentDto>(getDepartmentQueryById, new { departmentName });
        }

        public async Task<Department> UpdateAsync(DepartmentDto updatedDepartment)
        {
            //department.UpdatedBy = 1;
            //department.UpdatedDate = DateTime.UtcNow;
            //department.DepartmentId = departmentId;
            var departement = new Department
            {
                DepartmentName = updatedDepartment.DepartmentName,
                Description = updatedDepartment.Description,
                DepartmentId = updatedDepartment.DepartmentId,

            };
           _employeeManagementDataDbContext.Departments.Update(departement);
            _employeeManagementDataDbContext.SaveChanges();
            return departement;
        }

        public async Task<IEnumerable<EmployeeDto>> GetTotalNoOfEmpWorkingInEachDept(int deptId)
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
