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
    public class DeleteEmployee
    {
        private readonly IEmployeeService _employeeService;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly int _employeeId;

        public DeleteEmployee()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _employeeService = new EmployeeService(_mockUnitOfWork.Object, MapperFactory.GetMapper());
            _employeeId = EmployeeTestData.GetEmployeeId();
        }

        [Fact]
        public async Task DeleteEmployee_IdNotExist_NotFoundExceptionIsThrown()
        {
            _mockUnitOfWork
               .Setup(unitOfWork => unitOfWork.Employees.GetAsync(It.IsAny<int>()))
               .ReturnsAsync(await Task.FromResult(default(Employee)));

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await _employeeService.DeleteEmployeeAsync(_employeeId);
            });
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(0)]
        public async Task DeleteEmployee_InvalidId_UnprocessableEntityException(int id)
        {
            await Assert.ThrowsAsync<UnprocessableEntityException>(async () =>
            {
                await _employeeService.DeleteEmployeeAsync(id);
            });
        }

        [Fact]
        public async Task DeleteEmployee_MockRepositoryError_ExceptionIsThrown()
        {
            _mockUnitOfWork
               .Setup(unitOfWork => unitOfWork.Employees.GetAsync(It.IsAny<int>()))
               .ReturnsAsync(EmployeeTestData.GetEmployee_TestData().FirstOrDefault());

            _mockUnitOfWork
               .Setup(unitOfWork => unitOfWork.CompleteAsync())
               .ThrowsAsync(new Exception());

            await Assert.ThrowsAsync<Exception>(async () =>
            {
                await _employeeService.DeleteEmployeeAsync(_employeeId);
            });
        }
    }
}
