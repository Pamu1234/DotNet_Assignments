using ProjectManagement.Infrastructure.Services;
using Serilog;
using static ProjectManagement.Infrastructure.Validations.Validations;
using System.Collections.Generic;


ProjectManagementDataService employeeDataService = new ProjectManagementDataService();

// Logger

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/ProjectManagement.txt",rollingInterval:RollingInterval.Day)
    .CreateLogger();
Log.Information("Program execution is started");

string choiceValue;
do
{

    Console.WriteLine("Welcome to Project Management Data.");
    Console.WriteLine();
    Console.WriteLine("PRESS: 1 for display all departments data.");
    Console.WriteLine("PRESS: 2 for get departments details for the department id.");
    Console.WriteLine("PRESS: 3 for get department details for the department name.");
    Console.WriteLine("PRESS: 4 for get all projects for each department.");
    Console.WriteLine("PRESS: 5 for get the list of projects there for the department id.");
    Console.WriteLine("PRESS: 6 for get list of projects there for the department name");
    Console.WriteLine("PRESS: 7 for get all employee's data");
    Console.WriteLine("PRESS: 8 for get the list of employees's there for the department id.");
    Console.WriteLine("PRESS: 9 for get the employee's details for the employee id.");
    Console.WriteLine("PRESS: 10 for get display assignment data.");
    Console.WriteLine("PRESS: 11 for get the number of employee's working for each department.");
    Console.WriteLine("PRESS: 12 for get the total salary of employee's working for each department.");
    Console.WriteLine("PRESS: 13 for get Department Name, Project Name, Assignment Name, Employee Name.");
    Console.WriteLine("PRESS: 14 for get department id that you want data. (Please enter department id.)");
    Console.WriteLine("PRESS: 15 for get department name that you want data. (Please enter department name.)");
    Console.WriteLine("PRESS: 16 for Search Data.");
    Console.WriteLine();
    int choice = int.Parse(Console.ReadLine());
    Console.WriteLine();

    switch (choice)
    {
        case 1:
            // Display All departments data 

            Console.WriteLine("---Display All departments data----");
            var deptData = employeeDataService.GetDepartments();
            employeeDataService.DisplayDepartmentServiceData(deptData);
            Console.WriteLine();
            break;

        case 2:
            // Department details for the department Id.
            try
            {
                Console.WriteLine("Enter Department Id:");
                int deptId = Convert.ToInt32(Console.ReadLine());
                var deptIdData =  employeeDataService.GetDepartmentsData(deptId);
                Console.WriteLine();
                //if (!deptIdData.Any())
                //{
                //    
                //}
                if (Validate(deptIdData)) { 
                employeeDataService.DisplayDepartmentServiceData(deptIdData);
                    break;
                }
                Log.Debug($"Passing invalid dept id: {deptId}");
                throw new ArgumentOutOfRangeException();

            }
            catch (ArgumentOutOfRangeException a)
            {
                Log.Error(a,$"Something is wrong:{a.Message}");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            Console.WriteLine();
            break;

        case 3:
            // Department details for the Department Name
            try
            {
                Console.WriteLine("Enter Department Name:");
                string? deptName = Console.ReadLine();
                var deptNameData = employeeDataService.GetDepartmentsData(DepartmentName:deptName);
                if (!deptNameData.Any())
                {
                    Log.Debug($"Passing invalid department name: {deptName}");
                    throw new InvalidDataException("Invalid department name:");
                }
                employeeDataService.DisplayDepartmentServiceData(deptNameData);
            }
            catch (InvalidDataException i)
            {
                Log.Error(i, $"Something is wrong: {i.Message}, entering wrong department name.");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            Console.WriteLine();
            break;

        case 4:
            // All projects for each department.

            Console.WriteLine("---All projects for each department ----");
            var projectData = employeeDataService.GetProjects();
            employeeDataService.DisplayProjectData(projectData);
            //Log.Error("Getting Data");
            Console.WriteLine();
            break;

        case 5:
            // The list of projects there for the department Id.
            try
            {
                Console.WriteLine("Enter department id to see project list for specific department.");
                int deptid = Convert.ToInt32(Console.ReadLine());
                var deptIdinProject = employeeDataService.GetProjects(deptid);
                if (!deptIdinProject.Any())
                {
                    Log.Debug($"Enter wrong department id: {deptid}");
                    throw new ArgumentOutOfRangeException(nameof(deptIdinProject), "Enter valid department id");
                }
                employeeDataService.DisplayProjectData(deptIdinProject);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Log.Error(a, $"Something is wrong: {a.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            Console.WriteLine();
            break;

        case 6:
            // List of projects there for the Department Name.
            try
            {
                Console.WriteLine("Enter department name to see project list for specific department.");
                string deptName = Console.ReadLine();
                var deptNameinProject = employeeDataService.GetProjects(deptName: deptName);
                if (!deptNameinProject.Any())
                {
                    Log.Debug($"Passing invalid department name:{deptName} ");
                    throw new InvalidDataException("Invalid deptName");
                }
                employeeDataService.DisplayProjectData(deptNameinProject);
            }
            catch (InvalidDataException i)
            {
                Log.Error(i, $"Something is wrong: {i.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            Console.WriteLine();
            break;

        case 7:
            // All Employee Data.

            Console.WriteLine("---All Employee Data----");
            var empData = employeeDataService.GetEmployees();
            employeeDataService.DisplayEmployee(empData);
            Console.WriteLine();
            break;

        case 8:

            // The list of employees there for the department Id.

            //Console.WriteLine("The list of employees there for the department Id");
            try
            {
                Console.WriteLine("Enter department id: ");
                int empDataByPassingDeptId = Convert.ToInt32( Console.ReadLine());
                var empGetDataByPassingDeptId = employeeDataService.GetEmpData(empDataByPassingDeptId);
                if (!empGetDataByPassingDeptId.Any())
                {
                    Log.Debug($"Passing invalid depatment id: {empDataByPassingDeptId}");
                    throw new ArgumentOutOfRangeException(nameof(empGetDataByPassingDeptId), "Invalid department id:");
                }
                employeeDataService.DisplayEmployee(empGetDataByPassingDeptId);
            }
            catch (ArgumentOutOfRangeException a)
            {
                Log.Error(a, $"Something is wrong: {a.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            Console.WriteLine();
            break;

        case 9:
            //  The employees details for the Employee Id.

            Console.WriteLine("---The employees details for the Employee Id---");
            try
            {
                int empId = Convert.ToInt32( Console.ReadLine());
                var empGetDataByPassingEmpId = employeeDataService.GetEmpData(employeeNumber: empId);
                if (!empGetDataByPassingEmpId.Any())
                {
                    Log.Debug($"Passing invalid employee number: {empId}");
                    throw new ArgumentOutOfRangeException(nameof(empGetDataByPassingEmpId), "Invalid employee id:");
                }
                employeeDataService.DisplayEmployee(empGetDataByPassingEmpId);

            }
            catch (ArgumentOutOfRangeException a)
            {
               Log.Error(a,$"Something is wrong: {a.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            Console.WriteLine();
            break;

        case 10:

            // All Assignment Data.

            Console.WriteLine("---Display Assignment data----");
            var assignmentData = employeeDataService.GetAssignments();
            employeeDataService.DisplayAssignmentsData(assignmentData);
            Console.WriteLine();
            break;

        case 11:
            // Returning the number of employees working for each department

            Console.WriteLine("---Returning the number of employees working for each department---");
            employeeDataService.EmployeeCount();
            Console.WriteLine();
            break;

        case 12:
            // Returning the total salary of employees working for each department

            Console.WriteLine("---Returning the total salary of employees working for each department---");
            employeeDataService.EmployeeSalary();
            Console.WriteLine();
            break;

        case 13:

            // Query for returning DepartmentName, Project Name, Assignment Name, Employee Name.

            Console.WriteLine("\t\t---Returns DepartmentName, Project Name, Assignment Name, Employee Name---");
            var data = employeeDataService.GetDataByDepartNameProjectNameEmployeeName();
            foreach (var item in data)
            {
                Console.WriteLine($"{item.DepartmentName}\t{item.EmployeeName}\t{item.ProjectName}\t{item.AssignmentName}");
            }
            Console.WriteLine();
            break;

        case 14:
            //Query for returning result by departmentid wise.

            Console.WriteLine("Enter department id that you want data");

            try
            {
                int deptIdData = Convert.ToInt32(Console.ReadLine());

                if (deptIdData >0)
                {
                    employeeDataService.GetDataByDepartment(deptId: deptIdData);
                }
                else
                {
                    Log.Debug($"Passing invalid department id: {deptIdData}");
                    throw new ArgumentOutOfRangeException(nameof(deptIdData), "Please enter valid dept id");
                }

            }
            catch (ArgumentOutOfRangeException a)
            {
                Log.Error(a, $"Something is wrong: {a.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            Console.WriteLine();
            break;

        case 15:
            //Query for returning result by departmentname wise.

            Console.WriteLine("Enter department name that you want data");
            try
            {
                string? deptNameData = Console.ReadLine();


                if (string.IsNullOrWhiteSpace(deptNameData))
                {
                    Log.Debug($"Nothing is entred: {deptNameData}");
                    throw new FormatException("Department name can't be empty.");
                }
                if (deptNameData != "Marketing" && deptNameData != "Finance" && deptNameData != "Accounting")
                {
                    Log.Debug($"Entred data is miss-match: {deptNameData}");
                    throw new InvalidDataException("Please enter valid department name");
                }
                employeeDataService.GetDataByDepartment(deptName: deptNameData);
            }
            catch (FormatException f)
            {
                Log.Error(f, $"Something is wrong: {f.Message}");
            }
            catch (InvalidDataException i)
            {
                Log.Error(i, $"Something is wrong: {i.Message}");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
            }
            Console.WriteLine();
            break;
            
        case 16:
            Console.WriteLine("------------Search Data------------------");
            try
            {
                string searchData = Console.ReadLine().ToLower();
                if (string.IsNullOrWhiteSpace(searchData))
                {
                    Log.Debug($"Entering data is empty: {searchData}");
                    throw new InvalidDataException("Without enter any thing it is not possileble to search data");
                }

                var searchResult = employeeDataService.GetDataBySearchMethod(searchData);
                if (!searchResult.Any())
                {
                    Log.Debug($"Entering data is not matching: {searchResult}");
                    throw new ArgumentNullException("Data Match Not found");
                }
                foreach (var item in searchResult)
                {
                    Console.WriteLine($"{item.DepartmentName}\t{item.EmployeeName}\t{item.ProjectName}\t{item.AssignmentName}");
                }
            }
            catch (InvalidDataException i)
            {
                Log.Error(i,$"Something is wrong: {i.Message}");
            }
            catch (ArgumentNullException a)
            {
                Log.Error(a, $"Something is wrong: {a.Message}");
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            break;

        default:
            Console.WriteLine("Invalid choice!");
            break;

    }
    Console.WriteLine("Do you want to continue? Yes or No.");
    choiceValue=Console.ReadLine().ToLower();
} while (choiceValue=="yes");

Log.CloseAndFlush();
