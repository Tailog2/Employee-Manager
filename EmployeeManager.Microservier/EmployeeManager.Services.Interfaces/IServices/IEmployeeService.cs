using EmployeeManager.Domain.Core.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Services.Interfaces.IServices
{
    public interface IEmployeeService
    {
        public Task<IEnumerable<EmployeeDto>?> GetEmployeesAsync(string? query = null);
        public Task<EmployeeDto?> GetEmployeeAsync(int id);
        public Task<EmployeeDto> CreateEmployeeAsync(NewEmployeeDto employeeDto);
        public Task UpdateEmployeeAsync(int id, EditEmployeeDto employeeDto);
        public Task DeleteEmployeeAsync(int id);
    }
}
