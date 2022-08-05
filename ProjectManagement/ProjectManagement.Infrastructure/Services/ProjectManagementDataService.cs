using ProjectManagement.Core.Contract;
using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Model;
using System.Collections;
using static ProjectManagement.Infrastructure.Data.ProjectManagementDataInMemory;
namespace ProjectManagement.Infrastructure.Services
{
    public class ProjectManagementDataService : IProjectManagementReport
    {
        // Display whole department data.
        public void DisplayDepartmentServiceData(IEnumerable<Department> assignments)
        {
            var deptData = GetDepartments();
            foreach (var item in assignments)
            {
                Console.WriteLine($"{item.DepartmentId}\t{item.DepartmentName}\t{item.PhoneNumber}");
            }
        }

        // Query for display dept. details by passing dept. Id or dept. Name
        public IEnumerable<Department> GetDepartmentsData(int? DepartmentId = null, string? DepartmentName = null)
        {
            if (DepartmentId != null || DepartmentName != null)
            {
                var deptData = from emp in GetDepartments()
                               where (DepartmentId == null || emp.DepartmentId == DepartmentId)
                               && (DepartmentName == null || emp.DepartmentName == DepartmentName)
                               select emp;

                return deptData;
            }
            return GetDepartments();
        }

        // Project data
        public List<Project> GetProjects()
        {
            return project;
        }
        // Display whole project data.
        public void DisplayProjectData(IEnumerable<Project> project)
        {
            foreach (var projectItem in project)
            {
                Console.WriteLine($"{projectItem.ProjectId}\t{projectItem.DepartmentId}\t{projectItem.MaxHours}\t{projectItem.StartDate}\t{projectItem.EndDate}\t{projectItem.ProjectName}");
            }
        }

        // Display list of project 
        public IEnumerable<Project> GetProjects(int? deptId = null, string? deptName = null)
        {

            if (deptId != null || deptName != null)
            {
                var projectData = from pro in GetProjects()
                                  join dept in GetDepartments()
                                  on pro.DepartmentId equals dept.DepartmentId
                                  where (deptId == null || pro.DepartmentId == deptId)
                                  && (deptName == null || dept.DepartmentName == deptName)
                                  select pro;
                return projectData;
            }

            return GetProjects();

        }

        public List<Assignment> GetAssignments()
        {
            return assignments;
        }

        // Display whole Assignment data.
        public void DisplayAssignmentsData(IEnumerable<Assignment> assignments)
        {
            foreach (var item in assignments)
            {
                Console.WriteLine($"{item.ProjectId}\t{item.EmployeeNumber}\t{item.HourseWorked}\t{item.AssignmentName}");
            }
        }

        public List<Department> GetDepartments()
        {
            return department;
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        // Display whole Employee data.
        public void DisplayEmployee(IEnumerable<Employee> employee)
        {
            foreach (var item in employee)
            {
                Console.WriteLine($"{item.EmployeeNumber}\t{item.EmployeeName}\t{item.FirstName}\t{item.LastName}\t{item.DepartmentId}\t{item.Phone}\t{item.Salary}\t\t{item.Email}");
            }
        }

        // Display Emp id or Dept id
        public IEnumerable<Employee> GetEmpData(int? deptId = null, int? employeeNumber = null)
        {
            if (deptId != null || employeeNumber != null)
            {
                var empData = from emp in GetEmployees()
                              where (deptId == null || emp.DepartmentId == deptId)
                              && (employeeNumber == null || emp.EmployeeNumber == employeeNumber)
                              select emp;
                return empData;
            }
            return from emp in GetEmployees()
                   select emp;

        }

        // Method for returning the number of employees working for each department
        public IEnumerable EmployeeCount()
        {
            var empCount = from emp in GetEmployees()
                           group emp by emp.DepartmentId into empGroup
                           select new { emp = empGroup.Key, count = empGroup.Count() };
            foreach (var item in empCount)
            {
                Console.WriteLine($"Department Id: {item.emp}\t Total number of employees: {item.count}");
            }
            return empCount;

        }

        //Method for returning the total salary paid for each department
        public IEnumerable EmployeeSalary()
        {
            var empdata = from emp in GetEmployees()
                          group emp by emp.DepartmentId into g
                          select new { DepartmentId = g.Key, Count = g.Sum(x => x.Salary) };
            foreach (var item in empdata)
            {
                Console.WriteLine($"Department Id: {item.DepartmentId}\t Total salary of all employees:{item.Count}");
            }
            return empdata;
        }

        // Query for returning DepartmentName, Project Name, Assignment Name, Employee Name 

        public IEnumerable<ProjectResourceDetails> GetDataByDepartNameProjectNameEmployeeName()
        {
            var toatlData = (from dept in GetDepartments()
                             join pro in GetProjects()
                             on dept.DepartmentId equals pro.DepartmentId
                             join emp in GetEmployees()
                             on pro.DepartmentId equals emp.DepartmentId
                             join ass in GetAssignments()
                             on emp.EmployeeNumber equals ass.EmployeeNumber
                             select new
                             {
                                 DepartmentName = dept.DepartmentName,
                                 ProjectName = pro.ProjectName,
                                 AssignmentName = ass.AssignmentName,
                                 EmployeeName = emp.EmployeeName
                             }).Distinct();


            var data = from v in toatlData
                       select new ProjectResourceDetails() { DepartmentName = v.DepartmentName, EmployeeName = v.EmployeeName, ProjectName = v.ProjectName, AssignmentName = v.AssignmentName };

            return data;
        }

        public void GetDataByDepartment(int? deptId = null, string? deptName = null)
        {
            var getDeptData = from data in GetDataByDepartNameProjectNameEmployeeName()
                              join dept in GetDepartments() on data.DepartmentName equals dept.DepartmentName
                              where (deptId == null || dept.DepartmentId == (deptId)) && (deptName == null || data.DepartmentName.Contains(deptName))
                              select data;
            foreach (var dept in getDeptData)
            {
                Console.WriteLine($"DeptName :{dept.DepartmentName},\t Project Name: {dept.ProjectName},\t Assignment Name: {dept.AssignmentName},\tEmployee Name: {dept.EmployeeName}.");
            }
        }

        public IEnumerable<ProjectResourceDetails> GetDataBySearchMethod(string searchData)
        {

            var findData = from d in GetDataByDepartNameProjectNameEmployeeName()
                           where d.DepartmentName.ToLower().Contains(searchData) || d.EmployeeName.ToLower().Contains(searchData) || d.ProjectName.ToLower().Contains(searchData) || d.AssignmentName.ToLower().Contains(searchData)
                           select d;

            return findData;
        }

    }
}
