using EmployeeManager.Domain.Core.Dtos;
using EmployeeManager.Domain.Core.Exceptions;
using EmployeeManager.Domain.Interfaces.Common;
using EmployeeManager.Infrastructure.Business.Services;
using EmployeeManager.Infrastructure.Business.UnitTests.Factories;
using EmployeeManager.Infrastructure.Business.UnitTests.TestData;
using EmployeeManager.Services.Interfaces.Exceptions;
using EmployeeManager.Services.Interfaces.IServices;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Business.UnitTests.EmployeeServiceTest
{
    public class GetEmployees
    {
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly int _employeeId;

        public GetEmployees()
        {
            _employeeId = EmployeeTestData.GetEmployeeId();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _employeeService = new EmployeeService(_mockUnitOfWork.Object, MapperFactory.GetMapper());
        }

        [Fact]
        public async Task GetEmployee_Valid()
        {
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.GetAllAsync())
                .ReturnsAsync(EmployeeTestData.GetEmployee_TestData());

            var actual = await _employeeService.GetEmployeesAsync();

            Assert.True(actual != null);
            Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(actual);
        }

        [Fact]
        public async Task GetEmployee_ListOfEmployeeIsEmpty_ReturnEmptyList()
        {
            _mockUnitOfWork
               .Setup(unitOfWork => unitOfWork.Employees.GetAllAsync())
               .ReturnsAsync(new List<Employee>());

            var actual = await _employeeService.GetEmployeesAsync();

            Assert.True(actual != null);
            Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(actual);
            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetEmployee_UseQuery_ReturnList()
        {
            _mockUnitOfWork
               .Setup(unitOfWork => unitOfWork.Employees.FindAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
               .ReturnsAsync(EmployeeTestData.GetEmployee_TestData());

            var actual = await _employeeService.GetEmployeesAsync(EmployeeTestData.TestChar.ToString());

            Assert.True(actual != null);
            Assert.IsAssignableFrom<IEnumerable<EmployeeDto>>(actual);
            Assert.Equal(EmployeeTestData.GetEmployee_TestData().Count(), actual.Count());
        }

        [Fact]
        public async Task GetEmployee_MockRepositoryError_ExceptionIsThrown()
        {
            _mockUnitOfWork
               .Setup(unitOfWork => unitOfWork.Employees.GetAllAsync())
               .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var actual = await _employeeService.GetEmployeesAsync();
            });
        }
    }
}
