using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProjectManagement.Infrastructure.Validations.Validations;

namespace ProjectManagement.Infrastructure.Services
{
    public class ServiceValidations
    {
        ProjectManagementDataService employeeDataService = new ProjectManagementDataService();
        bool valid = false;

        public void MainMenu()
        {
            string choiceValue;
            do
            {
                try
                {
                    Console.WriteLine("Welcome to Project Management Data.");
                    Console.WriteLine();
                    Console.WriteLine("PRESS: 1 for get department data.");
                    Console.WriteLine("PRESS: 2 for get project data.");
                    Console.WriteLine("PRESS: 3 for get employee's data.");
                    Console.WriteLine("PRESS: 4 for get assignment data.");
                    Console.WriteLine("Press: 5 for get Department Name, Project Name, Assignment Name, Employee Name.");
                    Console.WriteLine("PRESS: 6 for get data by query.");
                    Console.WriteLine("PRESS: 7 for search data.");

                    Console.WriteLine();
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine();

                    switch (choice)
                    {
                        case 1:
                            // Display All departments data 
                            DepartmentDetails();
                            break;

                        case 2:
                            // All projects for each department.
                            ProjectData();
                            break;

                        case 3:
                            // Employee details.
                            EmployeeData();
                            break;

                        case 4:
                            // All Assignment Data.
                            AssignmentData();
                            break;

                        case 5:
                            // Query for returning DepartmentName, Project Name, Assignment Name, Employee Name.
                            GetDeptNameProjectNameAssignmentNameEmployeeName();
                            break;

                        case 6:
                            //Query for returning result by department id and department name wise.
                            GetDataByDeptIdAndDeptName();
                            break;

                        case 7:
                            SearchData();
                            break;
                            default:
                            Console.WriteLine("Invalid input");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Please select valid choice");
                }

                Console.WriteLine("Do you want to continue? Yes or No.");
                choiceValue = Console.ReadLine().ToLower();
            } while (choiceValue == "yes");
        }

        // Department Data:
        public void DepartmentDetails()
        {

            try
            {
                Console.WriteLine("PRESS: 1 for display all departments data.");
                Console.WriteLine("PRESS: 2 for get departments details for the department id.");
                Console.WriteLine("PRESS: 3 for get department details for the department name.");
                Console.WriteLine("PRESS: 4 for get total salary of employees working for each department");
                int dept = Convert.ToInt32(Console.ReadLine());
                switch (dept)
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
                        do
                        {
                            Console.WriteLine("Enter Department Id:");
                            int deptId = int.Parse(Console.ReadLine());
                            if (deptId < 0)
                            {
                                throw new InvalidDataException();
                            }

                            var deptIdData = employeeDataService.GetDepartmentsData(deptId);
                            Console.WriteLine();

                            //if (deptIdData.Any()
                            if(Validate(deptIdData))
                            {
                                employeeDataService.DisplayDepartmentServiceData(deptIdData);
                                valid = false;
                            }
                            else
                            {
                                Console.WriteLine("Data not found please enter valid input");
                                valid = true;
                            }

                        } while (valid);
                        Console.WriteLine();
                        break;

                    case 3:
                        // Department details for the Department Name

                        Console.WriteLine("Enter Department Name:");
                        string? deptName = Console.ReadLine();
                        var deptNameData = employeeDataService.GetDepartmentsData(DepartmentName: deptName);
                        if (!Validate(deptNameData))
                        {
                            //Log.Debug($"Passing invalid department name: {deptName}");
                            throw new InvalidDataException("Invalid department name:");
                        }
                        employeeDataService.DisplayDepartmentServiceData(deptNameData);
                        Console.WriteLine();
                        break;

                    case 4:
                        // Total salary of employees working for each department.
                        Console.WriteLine("---Returning the total salary of employees working for each department---");
                        employeeDataService.EmployeeSalary();
                        Console.WriteLine();
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                DepartmentDetails();
            }
            catch (InvalidDataException i)
            {
                Console.WriteLine($"Something is wrong: {i.Message}, entering wrong department name.");
                DepartmentDetails();
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine();
                DepartmentDetails();
            }
            catch (FormatException)
            {
                Console.WriteLine("Input can't be empty.");
                DepartmentDetails();
            }
            catch (Exception ex)
            {
                //Log.Error(ex.Message);
                DepartmentDetails();
            }
            do
            {
                Console.WriteLine("PRESS 1 : Department menu");
                Console.WriteLine("PRESS 2 : Main menu");
                int menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 1)
                {
                    DepartmentDetails();
                    valid = false;
                }
                if (menu == 2)
                {
                    MainMenu();
                    valid = false;
                    break;
                }
                else
                {
                    valid = true;
                }

            } while (valid);
        }
        // Project Data:
        public void ProjectData()
        {
            try
            {
                Console.WriteLine("PRESS: 1 for get all projects for each department.");
                Console.WriteLine("PRESS: 2 for get the list of projects there for the department id.");
                Console.WriteLine("PRESS: 3 for get list of projects there for the department name");

                int deptid = Convert.ToInt32(Console.ReadLine());
                switch (deptid)
                {
                    case 1:
                        // All projects for each department.
                        Console.WriteLine("---All projects for each department ----");
                        var projectData = employeeDataService.GetProjects();
                        employeeDataService.DisplayProjectData(projectData);
                        Console.WriteLine();
                        break;

                    case 2:
                        // The list of projects there for the department Id.
                        do
                        {
                            Console.WriteLine("Enter department id to see project list for specific department.");
                            int deptId = Convert.ToInt32(Console.ReadLine());
                            var deptIdinProject = employeeDataService.GetProjects(deptId);

                            if (deptId < 0)
                            {
                                throw new InvalidDataException();
                            }

                            var deptIdData = employeeDataService.GetDepartmentsData(deptId);
                            Console.WriteLine();

                            if (deptIdinProject.Any())
                            {
                                employeeDataService.DisplayProjectData(deptIdinProject);
                                valid = false;
                            }
                            else
                            {
                                Console.WriteLine("Data not found please enter valid input:");
                                valid = true;
                            }

                        } while (valid);
                        break;

                    case 3:
                        // List of projects there for the Department Name.

                        Console.WriteLine("Enter department name to see project list for specific department.");
                        string deptName = Console.ReadLine();
                        var deptNameinProject = employeeDataService.GetProjects(deptName: deptName);
                        if (!deptNameinProject.Any())
                        {
                            Console.WriteLine($"Passing invalid department name:{deptName} ");
                            throw new InvalidDataException("Invalid deptName");
                        }
                        employeeDataService.DisplayProjectData(deptNameinProject);

                        Console.WriteLine();
                        break;
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                ProjectData();
            }
            catch (FormatException)
            {
                Console.WriteLine("Input can't be empty.");
                ProjectData();
            }
            catch (InvalidDataException)
            {
                ProjectData();
            }
            catch (Exception)
            {
                ProjectData();
            }
            do
            {
                Console.WriteLine("PRESS 1 : Project menu");
                Console.WriteLine("PRESS 2 : Main menu");
                int menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 1)
                {
                    ProjectData();
                    valid = false;
                }
                if (menu == 2)
                {
                    MainMenu();
                    valid = false;
                    break;
                }
                else
                {
                    valid = true;
                }

            } while (valid);
        }
        // Employee Data.
        public void EmployeeData()
        {
            try
            {
                Console.WriteLine("PRESS: 1 for get all employee's data");
                Console.WriteLine("PRESS: 2 for get the list of employees's there for the department id.");
                Console.WriteLine("PRESS: 3 for get the employee's details for the employee id.");
                Console.WriteLine("PRESS: 4 for number of employee's working for each department.");

                int deptid = Convert.ToInt32(Console.ReadLine());
                switch (deptid)
                {
                    case 1:
                        // All Employee Data.

                        Console.WriteLine("---All Employee Data----");
                        var empData = employeeDataService.GetEmployees();
                        employeeDataService.DisplayEmployee(empData);
                        Console.WriteLine();
                        break;

                    case 2:

                        // The list of employees there for the department Id.

                        Console.WriteLine("The list of employees there for the department Id");

                        Console.WriteLine("Enter department id: ");
                        int empDataByPassingDeptId = Convert.ToInt32(Console.ReadLine());
                        var empGetDataByPassingDeptId = employeeDataService.GetEmpData(empDataByPassingDeptId);
                        if (!empGetDataByPassingDeptId.Any())
                        {
                            Console.WriteLine($"Passing invalid depatment id: {empDataByPassingDeptId}");
                            throw new ArgumentOutOfRangeException(nameof(empGetDataByPassingDeptId), "Invalid department id:");
                        }
                        employeeDataService.DisplayEmployee(empGetDataByPassingDeptId);
                        break;


                    case 3:

                        //  The employees details for the Employee Id.
                        do
                        {
                            Console.WriteLine("---The employees details for the Employee Id---");
                            int empId = Convert.ToInt32(Console.ReadLine());
                            if (empId < 0)
                            {
                                throw new InvalidDataException();
                            }
                            var empGetDataByPassingEmpId = employeeDataService.GetEmpData(employeeNumber: empId);

                            if (empGetDataByPassingEmpId.Any())
                            {
                                employeeDataService.DisplayEmployee(empGetDataByPassingEmpId); ;
                                valid = false;
                            }
                            else
                            {
                                Console.WriteLine("Data not found:");
                                valid = true;
                            }
                        }
                        while (valid);

                        break;

                    case 4:
                        // Returning the number of employees working for each department.
                        Console.WriteLine("---Returning the number of employees working for each department---");
                        employeeDataService.EmployeeCount();
                        Console.WriteLine();
                        break;

                    default: throw new ArgumentOutOfRangeException();
                }
            }
            catch (ArgumentOutOfRangeException a)
            {
                EmployeeData();
                Console.WriteLine($"Something is wrong: {a.Message}");
            }
            catch (InvalidDataException)
            {
                EmployeeData();
            }
            catch (FormatException)
            {
                EmployeeData();
                Console.WriteLine("Please enter department id.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            do
            {
                Console.WriteLine("PRESS 1 : Employee menu");
                Console.WriteLine("PRESS 2 : Main menu");
                int menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 1)
                {
                    EmployeeData();
                    valid = false;
                }
                if (menu == 2)
                {
                    MainMenu();
                    valid = false;
                    break;
                }
                else
                {
                    valid = true;
                }

            } while (valid);
        }
        // Assignment Data:
        public void AssignmentData()
        {
            Console.WriteLine("---Display Assignment data----");
            var assignmentData = employeeDataService.GetAssignments();
            employeeDataService.DisplayAssignmentsData(assignmentData);
            Console.WriteLine();

        }
        // Query for returning DepartmentName, Project Name, Assignment Name, Employee Name.

        public void GetDeptNameProjectNameAssignmentNameEmployeeName()
        {
            Console.WriteLine("\t\t---Returns DepartmentName, Project Name, Assignment Name, Employee Name---");
            var data = employeeDataService.GetDataByDepartNameProjectNameEmployeeName();
            foreach (var item in data)
            {
                Console.WriteLine($"{item.DepartmentName}\t{item.EmployeeName}\t{item.ProjectName}\t{item.AssignmentName}");
            }
            Console.WriteLine();
        }

        // Query data:
        public void GetDataByDeptIdAndDeptName()
        {
            try
            {
                //Query for returning result by department id and department name wise.
                Console.WriteLine("PRESS: 1 for get data after  entering department id (Enter department id that you want data)");
                Console.WriteLine("PRESS: 2 for get data after entering  department name that you want data (Enter department name that you want data)");

                int choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        try
                        {
                            do
                            {
                                Console.WriteLine("Enter department id that you want data");
                                int deptIdData = Convert.ToInt32(Console.ReadLine());
                                if (deptIdData < 0)
                                {
                                    throw new InvalidDataException();
                                }
                                var empGetDataByPassingEmpId = employeeDataService.GetEmpData(deptId: deptIdData);
                                if (empGetDataByPassingEmpId.Any())
                                {
                                    employeeDataService.DisplayEmployee(empGetDataByPassingEmpId);
                                    valid = false;
                                }
                                else
                                {
                                    Console.WriteLine("Data not found");
                                    valid = true;
                                }

                            } while (valid);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        Console.WriteLine();
                        break;

                    case 2:
                        //Query for returning result by department name wise.  
                        try
                        {
                            do
                            {
                                Console.WriteLine("Enter department name that you want data");
                                string? deptNameData = Console.ReadLine();
                                if (string.IsNullOrWhiteSpace(deptNameData))
                                {
                                    Console.WriteLine($"Nothing is entred: {deptNameData}");
                                    throw new FormatException("Department name can't be empty.");
                                }
                                if (deptNameData != "Marketing" && deptNameData != "Finance" && deptNameData != "Accounting")
                                {
                                    Console.WriteLine($"Entred data is miss-match: {deptNameData}");
                                    throw new InvalidDataException("Please enter valid department name");
                                }
                                employeeDataService.GetDataByDepartment(deptName: deptNameData);
                            } while (valid);
                        }
                        catch (Exception w)
                        {
                            Console.WriteLine("ggggggg");
                        }
                        Console.WriteLine();
                        break;
                }
            }
            catch (InvalidDataException)
            {
                Console.WriteLine("Hie");
            }
            do
            {
                Console.WriteLine("PRESS 1 : same menu");
                Console.WriteLine("PRESS 2 : Main menu");
                int menu = Convert.ToInt32(Console.ReadLine());
                if (menu == 1)
                {
                    GetDataByDeptIdAndDeptName();
                    valid = false;
                }
                if (menu == 2)
                {
                    MainMenu();
                    valid = false;
                    break;
                }
                else
                {
                    valid = true;
                }

            } while (valid);
        }

        public void SearchData()
        {
            Console.WriteLine("------------Search Data------------------");
            try
            {
                string searchData = Console.ReadLine().ToLower();
                if (string.IsNullOrWhiteSpace(searchData))
                {
                    Console.WriteLine($"Entering data is empty: {searchData}");
                    throw new InvalidDataException("Without enter any thing it is not possileble to search data");
                }

                var searchResult = employeeDataService.GetDataBySearchMethod(searchData);
                if (!searchResult.Any())
                {
                    Console.WriteLine($"Entering data is not matching: {searchResult}");
                    throw new ArgumentNullException("Data Match Not found");
                }
                foreach (var item in searchResult)
                {
                    Console.WriteLine($"{item.DepartmentName}\t{item.EmployeeName}\t{item.ProjectName}\t{item.AssignmentName}");
                }
            }
            catch (InvalidDataException i)
            {
                Console.WriteLine($"Something is wrong: {i.Message}");
            }
            catch (ArgumentNullException a)
            {
                Console.WriteLine($"Something is wrong: {a.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       
    }
}
