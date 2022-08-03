using ProjectManagement.Infrastructure.Services;
using System.Collections.Generic;

ProjectManagementDataService employeeDataService = new ProjectManagementDataService();


// Display All departments data 

Console.WriteLine("---Display All departments data----");
var deptData = employeeDataService.GetDepartments();
employeeDataService.DisplayDepartmentServiceData(deptData);
Console.WriteLine();

// Department details for the department Id.

Console.WriteLine("----Department details for the department Id---");
try
{
    var deptId = employeeDataService.GetDepartmentsData(2);

    if (!deptId.Any())
    {
        throw new ArgumentOutOfRangeException(nameof(deptId),"Invalid Department id");
    }
    employeeDataService.DisplayDepartmentServiceData(deptId);
}
catch(ArgumentOutOfRangeException d)
{
    Console.WriteLine(d.Message);
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}
Console.WriteLine();

// Department details for the Department Name

Console.WriteLine("---department details for the Department Name---");
try
{
    var deptName = employeeDataService.GetDepartmentsData(DepartmentName: "Finance");
    if(!deptName.Any())
    {
        throw new InvalidDataException("Invalid department name:");
    }
    employeeDataService.DisplayDepartmentServiceData(deptName);
}
catch(InvalidDataException i)
{
    Console.WriteLine(i.Message); ;
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine();


// All projects for each department.

Console.WriteLine("---All projects for each department ----");
var projectData = employeeDataService.GetProjects();
employeeDataService.DisplayProjectData(projectData);
Console.WriteLine();

// The list of projects there for the department Id.

Console.WriteLine("---The list of projects there for the department Id---");
try
{
    var deptIdinProject = employeeDataService.GetProjects(2);
    if(!deptIdinProject.Any())
    {
        throw new ArgumentOutOfRangeException(nameof(deptIdinProject),"Enter valid department id");
    }
    employeeDataService.DisplayProjectData(deptIdinProject);
}
catch(ArgumentOutOfRangeException a)
{
    Console.WriteLine(a.Message);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine();

// List of projects there for the Department Name.

Console.WriteLine("---List of projects there for the Department Name---");
try 
{
    var deptNameinProject = employeeDataService.GetProjects(deptName: "Accounting");
    if (!deptNameinProject.Any())
    {
        throw new InvalidDataException("Invalid deptName");
    }
    employeeDataService.DisplayProjectData(deptNameinProject);
}
catch (InvalidDataException i)
{
    Console.WriteLine(i.Message);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine();

// All Employee Data.

Console.WriteLine("---All Employee Data----");
var empData = employeeDataService.GetEmployees();
employeeDataService.DisplayEmployee(empData);
Console.WriteLine();

// The list of employees there for the department Id.

Console.WriteLine("The list of employees there for the department Id");
try
{
    var empGetDataByPassingDeptId = employeeDataService.GetEmpData(1);
    if(!empGetDataByPassingDeptId.Any())
    {
        throw new ArgumentOutOfRangeException(nameof(empGetDataByPassingDeptId), "Invalid department id:");
    }
    employeeDataService.DisplayEmployee(empGetDataByPassingDeptId);
}
catch(ArgumentOutOfRangeException a)
{
    Console.WriteLine(a.Message);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
Console.WriteLine();

//  The employees details for the Employee Id.

Console.WriteLine("---The employees details for the Employee Id---");
try
{
    var empGetDataByPassingEmpId = employeeDataService.GetEmpData(employeeNumber: 111);
    if (!empGetDataByPassingEmpId.Any())
    {
        throw new ArgumentOutOfRangeException(nameof(empGetDataByPassingEmpId), "Invalid employee id:");
    }
    employeeDataService.DisplayEmployee(empGetDataByPassingEmpId);

}
catch (ArgumentOutOfRangeException a)
{
    Console.WriteLine(a.Message);
}
catch (Exception ex) 
{ 
    Console.WriteLine(ex.Message); 
}
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
var data=employeeDataService.GetDataByDepartNameProjectNameEmployeeName();
foreach(var item in data)
{
    Console.WriteLine($"{item.DepartmentName}\t{item.EmployeeName}\t{item.ProjectName}\t{item.AssignmentName}");
}
Console.WriteLine();

//Query for returning result by departmentid wise.
Console.WriteLine("Enter departmentid that you want data");

try
{
    int deptIdData = Convert.ToInt32(Console.ReadLine());

    if (deptIdData <=0||deptIdData>3)
    {
        throw new ArgumentOutOfRangeException(nameof(deptIdData), "Please enter valid dept id");
    }
    employeeDataService.GetDataByDepartment(deptId: deptIdData);
}
catch (ArgumentOutOfRangeException a)
{
    Console.WriteLine(a.Message);
}
Console.WriteLine();

//Query for returning result by departmentname wise.

Console.WriteLine("Enter department name that you want data");
try
{
    string? deptNameData = Console.ReadLine();

    if (deptNameData!= "Marketing" && deptNameData!= "Finance" && deptNameData!= "Accounting")
    {
        throw new InvalidDataException("Please enter valid department name");
    }
    employeeDataService.GetDataByDepartment(deptName: deptNameData);

}
catch(InvalidDataException d) 
{
    Console.WriteLine(d.Message);
}
catch(Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine();
Console.WriteLine("------------Search Data------------------");
try
{
    string searchData = Console.ReadLine();
    if( !searchData.Any() )
    {
        throw new InvalidDataException("without enter any thing it is not possileble to search data ");
    }
   
    //Console.WriteLine("Data is found");
    var searchREasult=employeeDataService.GetDataBySearchMethod(searchData);
    if(!searchREasult.Any())
    {
        throw new ArgumentNullException("Data Match Not found");
    }
    foreach (var item in searchREasult)
    {
        Console.WriteLine($"{ item.DepartmentName}\t{ item.EmployeeName}\t{ item.ProjectName}\t{ item.AssignmentName}");
    }
}
catch(InvalidDataException i)
{
    Console.WriteLine(i.Message);
}
catch(ArgumentNullException anex)
{
    Console.WriteLine(anex.Message);
}
catch(Exception ex)
{
    Console.WriteLine(ex.Message);
}
