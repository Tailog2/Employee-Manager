using EmployeeManager.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Business.UnitTests.TestData
{
    internal static class EmployeeTestData
    {
        public const char TestChar = 'a';
        public const char InvalidTestChar = '-';
        private const int CharsCount = 10;

        public static List<NewEmployeeDto> CreateEmployee_TestData()
        {
            return new List<NewEmployeeDto>()
            {
                new NewEmployeeDto()
                {
                    FirstName = new string(TestChar, CharsCount),
                    LastName = new string(TestChar, CharsCount),
                    DepartmentId = 1,
                    RolesIds = new List<int>() { 1, 2 }
                }
            };
        }

        public static List<NewEmployeeDto> CreateEmployee_InvalidTestData()
        {
            return new List<NewEmployeeDto>()
            {
                new NewEmployeeDto()
                {
                    FirstName = new string(InvalidTestChar, CharsCount),
                    LastName = new string(InvalidTestChar, CharsCount),
                    DepartmentId = 1,
                    RolesIds = new List<int>() { 1, 2 }
                }
            };
        }

        public static List<EditEmployeeDto> UpdateEmployee_TestData()
        {
            return new List<EditEmployeeDto>()
            {
                new EditEmployeeDto()
                {
                    Id = 1,
                    FirstName = new string(TestChar, CharsCount),
                    LastName = new string(TestChar, CharsCount),
                    DepartmentId = 1,
                    RolesIds = new List<int>() { 1, 2 }
                }
            };
        }

        public static List<EditEmployeeDto> UpdateEmployee_InvalidTestData()
        {
            return new List<EditEmployeeDto>()
            {
                new EditEmployeeDto()
                {
                    Id = 1,
                    FirstName = new string(InvalidTestChar, CharsCount),
                    LastName = new string(InvalidTestChar, CharsCount),
                    DepartmentId = 1,
                    RolesIds = new List<int>() { 1, 2 }
                }
            };
        }

        public static List<Employee> GetEmployee_TestData()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    FirstName = new string(TestChar, CharsCount),
                    LastName = new string(TestChar, CharsCount),
                    Department = new Department { Id = 1, Name = new string(TestChar, CharsCount) },
                    EmployeeRole = new List<EmployeeRole>()
                    {
                        new EmployeeRole() { EmployeeId = 1, RoleId =1},
                        new EmployeeRole() { EmployeeId = 1, RoleId =2}
                    }
                }
            };
        }

        public static List<Employee> GetEmployees_TestData()
        {
            return new List<Employee>()
            {
                new Employee()
                {
                    FirstName = new string(TestChar, CharsCount),
                    LastName = new string(TestChar, CharsCount),
                    Department = new Department { Id = 1, Name = new string(TestChar, CharsCount) },
                    EmployeeRole = new List<EmployeeRole>()
                    {
                        new EmployeeRole() { EmployeeId = 1, RoleId =1},
                        new EmployeeRole() { EmployeeId = 1, RoleId =2}
                    }
                }
            };
        }

        public static int GetEmployeeId()
        {
            return 1;
        }
        public static List<int> GetEmployeeRolesIds()
        {
            return new List<int>() { 1, 2 };
        }
    }
}
