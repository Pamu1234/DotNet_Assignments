﻿using ProjectManagement.Core.Entities;

namespace ProjectManagement.Infrastructure.Data
{
    public static class ProjectManagementDataInMemory
    {
        public static List<Assignment> assignments = new()
        {
            new Assignment {ProjectId=1000,EmployeeNumber=111,HourseWorked=50,AssignmentName="Reports"},
            new Assignment {ProjectId=1000,EmployeeNumber=112,HourseWorked=100,AssignmentName="case study"},
            new Assignment { ProjectId = 1000, EmployeeNumber = 113, HourseWorked = 50,AssignmentName = "FileSystem" },
            new Assignment { ProjectId = 1001, EmployeeNumber = 114, HourseWorked = 150,AssignmentName="Linq" },
            new Assignment { ProjectId = 1001, EmployeeNumber = 112, HourseWorked = 100,AssignmentName = "case study" },
            new Assignment { ProjectId = 1001, EmployeeNumber = 111, HourseWorked = 140,AssignmentName = "Reports" },
            new Assignment { ProjectId = 1002, EmployeeNumber = 113, HourseWorked = 95,AssignmentName = "FileSystem" },
            new Assignment { ProjectId = 1002, EmployeeNumber = 112, HourseWorked = 85,AssignmentName = "case study" },
            new Assignment { ProjectId = 1002, EmployeeNumber = 111, HourseWorked = 78,AssignmentName = "Reports" },
            new Assignment { ProjectId = 1003, EmployeeNumber = 112, HourseWorked = 250,AssignmentName = "case study" },
            new Assignment { ProjectId = 1003, EmployeeNumber = 114, HourseWorked = 125,AssignmentName = "Linq" },
            new Assignment { ProjectId = 1003, EmployeeNumber = 111, HourseWorked = 150,AssignmentName = "Reports" },
            new Assignment { ProjectId = 1004, EmployeeNumber = 115, HourseWorked = 80,AssignmentName = "Collections" },
            new Assignment { ProjectId = 1004, EmployeeNumber = 116, HourseWorked = 70,AssignmentName = "ASP" },
            new Assignment { ProjectId = 1004, EmployeeNumber = 117, HourseWorked = 90,AssignmentName = "Directory" },
            new Assignment { ProjectId = 1004, EmployeeNumber = 118, HourseWorked = 90,AssignmentName = "Testing" },
            new Assignment { ProjectId = 1005, EmployeeNumber = 115, HourseWorked = 50,AssignmentName="Collections" },
            new Assignment { ProjectId = 1005, EmployeeNumber = 116, HourseWorked = 300,AssignmentName="ASP" },
            new Assignment { ProjectId = 1005, EmployeeNumber = 117, HourseWorked = 100,AssignmentName="Directory" },
            new Assignment { ProjectId = 1005, EmployeeNumber = 115, HourseWorked = 140,AssignmentName="Collections" },
            new Assignment { ProjectId = 1006, EmployeeNumber = 116, HourseWorked = 85,AssignmentName="ASP" },
            new Assignment { ProjectId = 1006, EmployeeNumber = 118, HourseWorked = 95,AssignmentName="Testing" },
            new Assignment { ProjectId = 1006, EmployeeNumber = 117, HourseWorked = 78,AssignmentName="Directory" },
            new Assignment { ProjectId = 1006, EmployeeNumber = 116, HourseWorked = 250,AssignmentName="ASP" },
            new Assignment { ProjectId = 1007, EmployeeNumber = 119, HourseWorked = 125,AssignmentName="Typing" },
            new Assignment { ProjectId = 1007, EmployeeNumber = 120, HourseWorked = 150,AssignmentName="Network" },
            new Assignment { ProjectId = 1007, EmployeeNumber = 121, HourseWorked = 80,AssignmentName="Design" },
            new Assignment { ProjectId = 1007, EmployeeNumber = 122, HourseWorked = 140,AssignmentName="Database" },
            new Assignment { ProjectId = 1008, EmployeeNumber = 119, HourseWorked = 85,AssignmentName="Typing" },
            new Assignment { ProjectId = 1008, EmployeeNumber = 120, HourseWorked = 95,AssignmentName="Network" },
            new Assignment { ProjectId = 1008, EmployeeNumber = 121, HourseWorked = 78,AssignmentName="Design" },
            new Assignment { ProjectId = 1009, EmployeeNumber = 119, HourseWorked = 250,AssignmentName="Typing" },
            new Assignment { ProjectId = 1009, EmployeeNumber = 120, HourseWorked = 125,AssignmentName="Network" },
            new Assignment { ProjectId = 1009, EmployeeNumber = 121, HourseWorked = 150,AssignmentName="Design" },
            new Assignment { ProjectId = 1010, EmployeeNumber = 119, HourseWorked = 80,AssignmentName="Typing" },
            new Assignment { ProjectId = 1010, EmployeeNumber = 120, HourseWorked = 60,AssignmentName="Network" },
            new Assignment { ProjectId = 1011, EmployeeNumber = 121, HourseWorked = 80,AssignmentName="Design" }
        };

        public static List<Department> department = new()
        {
                new () {DepartmentId = 1,DepartmentName = "Marketing",PhoneNumber = 9292929292 },
                new () {DepartmentId = 2,DepartmentName = "Finance",PhoneNumber = 9292929293 },
                new () {DepartmentId = 3,DepartmentName = "Accounting",PhoneNumber = 9292929294 }
        };

        public static List<Employee> employees = new()
        {

            new () {EmployeeNumber = 111,EmployeeName = "RahulKumar",FirstName="Rahul",LastName="Kumar",DepartmentId=1,Phone=123456789,Email="test@gmail.com",Salary=10000},
            new () {EmployeeNumber = 112,EmployeeName = "SunilKumar",FirstName="Sunil",LastName="Kumar",DepartmentId=1,Phone=223456789,Email="test@gmail.com",Salary=15000},
            new () {EmployeeNumber = 113,EmployeeName = "ManiBupal",FirstName="Mani",LastName="Bupal",DepartmentId=1,Phone=323456789,Email="test2@gmail.com",Salary=20000},
            new () {EmployeeNumber = 114,EmployeeName = "RaviKrishna",FirstName="Ravi",LastName="Krishna",DepartmentId=1,Phone=423456789,Email="test3@gmail.com",Salary=15000},
            new () {EmployeeNumber = 115,EmployeeName = "SirishKarri",FirstName="Sirish",LastName="Karri",DepartmentId=2,Phone=823456789,Email="test4@gmail.com",Salary=10000},
            new () {EmployeeNumber = 116,EmployeeName = "Raju",FirstName="Raju",LastName="Karri",DepartmentId=2,Phone=623456789,Email="test5@gmail.com",Salary=15000},
            new () {EmployeeNumber = 117,EmployeeName = "Naidu",FirstName="Naidu",LastName="Karri",DepartmentId=2,Phone=723456789,Email="test6@gmail.com",Salary=20000},
            new () {EmployeeNumber = 118,EmployeeName = "Siva",FirstName="Siva",LastName="Karri",DepartmentId=2,Phone=923456789,Email="test7@gmail.com",Salary=15000},
            new () {EmployeeNumber = 119,EmployeeName = "Hemanth",FirstName="Hemanth",LastName="Pilla",DepartmentId=3,Phone=133456789,Email="test8@gmail.com",Salary=10000},
            new () {EmployeeNumber = 120,EmployeeName = "Venkat",FirstName="Venkat",LastName="Pilla",DepartmentId=3,Phone=243456789,Email="test9@gmail.com",Salary=15000},
            new () {EmployeeNumber = 121,EmployeeName = "Vanakar",FirstName = "Vanakar",LastName ="Pilla", DepartmentId =3,Phone=353456789,Email = "test71@gmail.com", Salary=20000 },
            new () {EmployeeNumber = 122,EmployeeName = "Chinna",FirstName = "Chinna",LastName ="Pilla", DepartmentId =3,Phone=463456789,Email = "test72@gmail.com", Salary=15000 },
            new () {EmployeeNumber = 123,EmployeeName = "Kishore",FirstName = "Kishore",LastName ="Karri", DepartmentId =3,Phone=873456789,Email = "test73@gmail.com", Salary=10000 },
            new () {EmployeeNumber = 124,EmployeeName = "Harish",FirstName = "Harish",LastName ="Karri", DepartmentId =3,Phone=683456789,Email = "test74@gmail.com", Salary=15000 },
            new () {EmployeeNumber = 125,EmployeeName = "Thusanth",FirstName = "Thusanth",LastName ="Pilla", DepartmentId =1,Phone=729456789,Email = "test75@gmail.com", Salary=20000 },
            new () {EmployeeNumber = 126,EmployeeName = "Hanvika",FirstName = "Hanvika",LastName ="Pilla", DepartmentId =2,Phone=928456789,Email = "test76@gmail.com", Salary=15000 },
            new () {EmployeeNumber = 127,EmployeeName = "Shanvi",FirstName = "Shanvi",LastName ="Pilla", DepartmentId =3,Phone=127456789,Email = "test77@gmail.com", Salary=10000 },
            new () {EmployeeNumber = 128,EmployeeName = "Vedha",FirstName = "Vedha",LastName ="Pilla", DepartmentId =1,Phone=226456789,Email = "test78@gmail.com", Salary=15000 },
            new () {EmployeeNumber = 129,EmployeeName = "Jeshna",FirstName = "Jeshna",LastName ="Polimera", DepartmentId =2,Phone=325456789,Email = "test99@gmail.com", Salary=20000 },
            new () {EmployeeNumber = 130,EmployeeName = "Rama",FirstName = "Rama",LastName ="Polimera", DepartmentId =3,Phone=424456789,Email = "test79@gmail.com", Salary=15000 }

        };

        public static List<Project> project = new()
        {

            new () {ProjectId = 1000,ProjectName="2022 Q1 Product Plan",DepartmentId=1, MaxHours = 500, StartDate = new DateOnly(2022,1,1), EndDate = new DateOnly(2022,3,31) },
            new () {ProjectId = 1001,ProjectName="2022 Q2 Product Plan",DepartmentId=1, MaxHours = 600, StartDate = new DateOnly(2022,4,1), EndDate = new DateOnly(2022,7,31) },
            new () {ProjectId = 1002,ProjectName="2022 Q3 Product Plan",DepartmentId=1, MaxHours = 700, StartDate = new DateOnly(2022,7,1), EndDate = new DateOnly(2022,10,31) },
            new () {ProjectId = 1003,ProjectName="2022 Q4 Product Plan",DepartmentId=1, MaxHours = 800, StartDate = new DateOnly(2022,10,1),EndDate =new DateOnly(2022,12,31) },
            new () {ProjectId = 1004,ProjectName="2022 Q1 Portfolio Analysis",DepartmentId=2, MaxHours = 300, StartDate = new DateOnly(2022,1,1), EndDate =new DateOnly(2022,3,31) },
            new () {ProjectId = 1005,ProjectName="2022 Q2 Portfolio Analysis",DepartmentId=2, MaxHours = 400, StartDate = new DateOnly(2022,4,1), EndDate =new DateOnly(2022,7,31) },
            new () {ProjectId = 1006,ProjectName="2022 Q3 Portfolio Analysis",DepartmentId=2, MaxHours = 900, StartDate = new DateOnly(2022,7,1), EndDate =new DateOnly(2022,10,31) },
            new () {ProjectId = 1007,ProjectName="2022 Q4 Portfolio Analysis",DepartmentId=2, MaxHours = 800, StartDate = new DateOnly(2022,10,1),EndDate = new DateOnly(2022,12,31) },
            new () {ProjectId = 1008,ProjectName="2022 Q1 Tax Preparation",DepartmentId=3, MaxHours = 700, StartDate = new DateOnly(2022,1,1), EndDate =new DateOnly(2022,3,31) },
            new () {ProjectId = 1009,ProjectName="2022 Q2 Tax Preparation",DepartmentId=3, MaxHours = 850, StartDate = new DateOnly(2022,4,1), EndDate =new DateOnly(2022,7,31) },
            new () {ProjectId = 1010,ProjectName="2022 Q3 Tax Preparation",DepartmentId=3, MaxHours = 900, StartDate = new DateOnly(2022,7,1), EndDate =new DateOnly(2022,10,31) },
            new () {ProjectId = 1011,ProjectName="2022 Q4 Tax Preparation",DepartmentId=3, MaxHours = 800, StartDate = new DateOnly(2022,10,1),EndDate =new DateOnly(2022,12,31) }
        };
    }
}
