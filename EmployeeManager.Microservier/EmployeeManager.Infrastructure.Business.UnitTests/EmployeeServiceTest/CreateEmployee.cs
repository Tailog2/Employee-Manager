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
    public class CreateEmployee
    {
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;


        public CreateEmployee()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _employeeService = new EmployeeService(_mockUnitOfWork.Object, MapperFactory.GetMapper());
        }

        [Fact]
        public async Task CreateEmployee_Valid()
        {
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.ExistsAsync(It.IsAny<Expression<Func<Employee,bool>>>()))
                .Returns(Task.FromResult(false));
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Departments.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(await Task.FromResult(It.IsAny<Department>()));
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Roles.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(await Task.FromResult(It.IsAny<Role>()));
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.FromResult(It.IsAny<Employee>()));

            var actual = await _employeeService.CreateEmployeeAsync(EmployeeTestData.CreateEmployee_TestData().FirstOrDefault());

            Assert.True(actual != null);
            Assert.IsType<EmployeeDto>(actual);
        }

        [Fact]
        public async Task CreateEmployee_InvalidFullName_BadRequestExceptionIsThrown()
        {
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.ExistsAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                .Returns(Task.FromResult(false));
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Departments.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(await Task.FromResult(It.IsAny<Department>()));
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Roles.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(await Task.FromResult(It.IsAny<Role>()));
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.AddAsync(It.IsAny<Employee>()))
                .Returns(Task.FromResult(It.IsAny<Employee>()));

            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                var actual = await _employeeService.CreateEmployeeAsync(EmployeeTestData.CreateEmployee_InvalidTestData().FirstOrDefault());
            });
        }

        [Fact]
        public async Task GetEmployee_SameFullNameExists_ConflictExceptionIsThrown()
        {
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.ExistsAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                .Returns(Task.FromResult(true));

            await Assert.ThrowsAsync<BadRequestException>(async () =>
            {
                var actual = await _employeeService.CreateEmployeeAsync(EmployeeTestData.CreateEmployee_InvalidTestData().FirstOrDefault());
            });
        }

        [Fact]
        public async Task CreateEmployee_MockRepositoryError_ExceptionIsThrown()
        {
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.ExistsAsync(It.IsAny<Expression<Func<Employee, bool>>>()))
                .ReturnsAsync(false);
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Departments.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Department>());
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Roles.GetAsync(It.IsAny<int>()))
                .ReturnsAsync(It.IsAny<Role>());
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.AddAsync(It.IsAny<Employee>()))
                .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var actual = await _employeeService.CreateEmployeeAsync(EmployeeTestData.CreateEmployee_TestData().FirstOrDefault());
            });
        }
    }
}
