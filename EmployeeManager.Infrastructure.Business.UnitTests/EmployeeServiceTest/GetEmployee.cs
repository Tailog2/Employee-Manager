using EmployeeManager.Domain.Core.Dtos;
using EmployeeManager.Domain.Core.Exceptions;

using EmployeeManager.Domain.Interfaces.Common;
using EmployeeManager.Infrastructure.Business.Services;
using EmployeeManager.Infrastructure.Business.UnitTests.Factories;
using EmployeeManager.Infrastructure.Business.UnitTests.TestData;
using EmployeeManager.Infrastructure.Data;
using EmployeeManager.Services.Interfaces.Exceptions;
using EmployeeManager.Services.Interfaces.IServices;
using Moq;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;

namespace EmployeeManager.Infrastructure.Business.UnitTests.EmployeeServiceTest
{
    public class GetEmployee
    {
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly int _employeeId;

        public GetEmployee()
        {
            _employeeId = EmployeeTestData.GetEmployeeId();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _employeeService = new EmployeeService(_mockUnitOfWork.Object, MapperFactory.GetMapper());
        }

        [Fact]
        public async Task GetEmployee_Valid()
        {
            _mockUnitOfWork
                .Setup(unitOfWork => unitOfWork.Employees.GetAsync(It.IsAny<int>()))
                .Returns(Task.FromResult(EmployeeTestData.GetEmployee_TestData().FirstOrDefault()));

            var actual = await _employeeService.GetEmployeeAsync(_employeeId);

            Assert.True(actual != null);
            Assert.IsType<EmployeeDto>(actual);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        public async Task GetEmployee_InvalidId_UnprocessableEntityException(int id)
        {
            await Assert.ThrowsAsync<UnprocessableEntityException>(async () =>
            {
                var actual = await _employeeService.GetEmployeeAsync(id);
            });
        }

        [Fact]
        public async Task GetEmployee_IdNotExist_NotFoundExceptionIsThrown()
        {
            _mockUnitOfWork
               .Setup(unitOfWork => unitOfWork.Employees.GetAsync(It.IsAny<int>()))
               .Returns(Task.FromResult(default(Employee)));

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                var actual = await _employeeService.GetEmployeeAsync(_employeeId);
            });
        }

        [Fact]
        public async Task GetEmployee_MockRepositoryError_ExceptionIsThrown()
        {
            _mockUnitOfWork
               .Setup(unitOfWork => unitOfWork.Employees.GetAsync(It.IsAny<int>()))
               .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                var actual = await _employeeService.GetEmployeeAsync(_employeeId);
            });
        }
    }
}