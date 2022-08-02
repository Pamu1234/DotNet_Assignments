using ProjectManagement.Infrastructure.Services;


ProjectManagementDataService employeeDataService = new ProjectManagementDataService();


// Display All departments data 

Console.WriteLine("---Display All departments data----");
var deptData = employeeDataService.GetDepartments();
employeeDataService.DisplayDepartmentServiceData(deptData);
Console.WriteLine();

// Department details for the department Id.

Console.WriteLine("----Department details for the department Id---");
var deptId = employeeDataService.GetDepartmentsData(1);
employeeDataService.DisplayDepartmentServiceData(deptId);
Console.WriteLine();

// Department details for the Department Name

Console.WriteLine("---department details for the Department Name---");
var deptName = employeeDataService.GetDepartmentsData(DepartmentName: "Finance");
employeeDataService.DisplayDepartmentServiceData(deptId);
Console.WriteLine();


// All projects for each department.
// 
Console.WriteLine("---All projects for each department ----");
var projectData = employeeDataService.GetProjects();
employeeDataService.DisplayProjectData(projectData);
Console.WriteLine();

// The list of projects there for the department Id.

Console.WriteLine("---The list of projects there for the department Id---");
var deptIdinProject = employeeDataService.GetProjects(1);
employeeDataService.DisplayProjectData(deptIdinProject);
Console.WriteLine();

// List of projects there for the Department Name.

Console.WriteLine("---List of projects there for the Department Name---");
var deptNameinProject = employeeDataService.GetProjects(deptName: "Accounting");
employeeDataService.DisplayProjectData(deptNameinProject);
Console.WriteLine();

// All Employee Data.

Console.WriteLine("---All Employee Data----");
var empData = employeeDataService.GetEmployees();
employeeDataService.DisplayEmployee(empData);
Console.WriteLine();

// The list of employees there for the department Id.

Console.WriteLine("The list of employees there for the department Id");
var empGetDataByPassingDeptId = employeeDataService.GetEmpData(1);
employeeDataService.DisplayEmployee(empGetDataByPassingDeptId);
Console.WriteLine();

//  The employees details for the Employee Id.

Console.WriteLine("---The employees details for the Employee Id---");
var empGetDataByPassingEmpId = employeeDataService.GetEmpData(employeeNumber: 111);
employeeDataService.DisplayEmployee(empGetDataByPassingEmpId);
Console.WriteLine();

// All Assignment Data.

Console.WriteLine("---Display Assignment data----");
var assignmentData = employeeDataService.GetAssignments();
employeeDataService.DisplayAssignmentsData(assignmentData);
Console.WriteLine();

// Returning the number of employees working for each department

Console.WriteLine("---Returning the number of employees working for each department---");
employeeDataService.EmployeeCount();
Console.WriteLine();

// Returning the total salary of employees working for each department

Console.WriteLine("---Returning the total salary of employees working for each department---");
employeeDataService.EmployeeSalary();
Console.WriteLine();

// Query for returning DepartmentName, Project Name, Assignment Name, Employee Name. 
Console.WriteLine("\t\t---Returns DepartmentName, Project Name, Assignment Name, Employee Name---");
employeeDataService.GetDataByDepartNameProjectNameEmployeeName();
Console.WriteLine();

//Query for returning result by departmentid wise.

Console.WriteLine("Enter departmentid that you want data");
int deptIdData = Convert.ToInt32(Console.ReadLine());
employeeDataService.GetDataByDepartNameProjectNameEmployeeName(deptId: deptIdData);
Console.WriteLine();

//Query for returning result by departmentname wise.

Console.WriteLine("Enter department name that you want data");
string? deptNameData = Console.ReadLine();
employeeDataService.GetDataByDepartNameProjectNameEmployeeName(deptName: deptNameData);
Console.WriteLine();

//Query for returning result by project wise.

Console.WriteLine("Enter project name that you want data");
string? projectNameData = Console.ReadLine();
employeeDataService.GetDataByDepartNameProjectNameEmployeeName(projectName: projectNameData);
Console.WriteLine();

//Query for returning result by assignment wise.

Console.WriteLine("Enter assignment name that you want data");
string? assignmentNameData = Console.ReadLine();
employeeDataService.GetDataByDepartNameProjectNameEmployeeName(assignmentName: assignmentNameData);

//Query for returning result by employee name wise.

Console.WriteLine("Enter employee name that you want data");
string? employeeNameData = Console.ReadLine();
employeeDataService.GetDataByDepartNameProjectNameEmployeeName(employeeName: employeeNameData);
