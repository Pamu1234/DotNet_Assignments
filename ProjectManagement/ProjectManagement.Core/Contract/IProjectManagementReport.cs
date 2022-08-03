using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Model;
using System.Collections;

namespace ProjectManagement.Core.Contract
{
    public interface IProjectManagementReport
    {
        public List<Assignment> GetAssignments();

        public List<Department> GetDepartments();

        public List<Employee> GetEmployees();

        public List<Project> GetProjects();

        public void DisplayAssignmentsData(IEnumerable<Assignment> assignments);

        public IEnumerable<Project> GetProjects(int? deptId = null, string? deptName = null);

        public IEnumerable<Department> GetDepartmentsData(int? DepartmentId = null, string? DepartmentName = null);

        public IEnumerable<Employee> GetEmpData(int? deptId = null, int? employeeId = null);

        public IEnumerable EmployeeCount();

        public IEnumerable EmployeeSalary();

        public IEnumerable<ProjectResourceDetails> GetDataByDepartNameProjectNameEmployeeName();
        public IEnumerable<ProjectResourceDetails> GetDataBySearchMethod(string searchData);
    }
}
