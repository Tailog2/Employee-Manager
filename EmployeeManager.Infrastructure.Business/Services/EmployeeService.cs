using AutoMapper;
using EmployeeManager.Domain.Core.Constants;
using EmployeeManager.Domain.Core.Dtos;
using EmployeeManager.Domain.Core.Enums;
using EmployeeManager.Domain.Core.Exceptions;
using EmployeeManager.Domain.Core.Models;
using EmployeeManager.Domain.Interfaces.Common;
using EmployeeManager.Services.Interfaces.Exceptions;
using EmployeeManager.Services.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Infrastructure.Business.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(NewEmployeeDto employeeDto)
        {
            throw new Exception("Some exception");

            var employee = _mapper.Map<NewEmployeeDto, Employee>(employeeDto);

            if (!FullnameIsLetters(employee))
            {
                throw new BadRequestException(ValidationProblems.FullNameOnlyLetters);
            }               
            if (!(await FullnameIsDistinct(employee)))
            {
                throw new BadRequestException(ValidationProblems.FullNameСollision);
            }              
            if (!(await IsNotCio(employee)))
            {
                throw new BadRequestException(ValidationProblems.FullNameСollision);
            }

            try
            {
                await _unitOfWork.Employees.AddAsync(employee);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                if (await _unitOfWork.Employees.ExistsAsync(e => e.Id == employee.Id))
                {
                    throw new ConflictException();
                }

                throw new Exception();
            }

            await AttachDepartment(employee);
            await AttachRoles(employee);

            return _mapper.Map<Employee, EmployeeDto>(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            if (id <= 0)
            {
                throw new UnprocessableEntityException(RequestProblems.IncorrectId);
            }
                
            var employee = await _unitOfWork.Employees.GetAsync(id);

            if (employee is null)
            {
                throw new NotFoundException();
            }               

            _unitOfWork.Employees.Remove(employee);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<EmployeeDto?> GetEmployeeAsync(int id)
        {
            if (id <= 0)
            {
                throw new UnprocessableEntityException(RequestProblems.IncorrectId);
            }              

            var employee = await _unitOfWork.Employees.GetAsync(id);

            if (employee is null)
            {
                throw new NotFoundException();
            }           

            await _unitOfWork.CompleteAsync();
            return _mapper.Map<Employee, EmployeeDto>(employee);
        }

        public async Task<IEnumerable<EmployeeDto>?> GetEmployeesAsync(string? query = null)
        {
            IEnumerable<Employee>? employees = null;

            if (string.IsNullOrWhiteSpace(query) == false)
            {
                employees = await _unitOfWork.Employees.FindAsync(e => e.FirstName.Contains(query) || e.LastName.Contains(query));
            }
            else
            {
                employees = await _unitOfWork.Employees.GetAllAsync();
            }

            await _unitOfWork.CompleteAsync();
            return employees?.Select(e => _mapper.Map<Employee, EmployeeDto>(e));
        }

        public async Task UpdateEmployeeAsync(int id, EditEmployeeDto employeeDto)
        {
            var employee = _mapper.Map<EditEmployeeDto, Employee>(employeeDto);

            if (id != employee.Id)
            {
                throw new BadRequestException();
            }              
            if (id <= 0)
            {
                throw new UnprocessableEntityException(RequestProblems.IncorrectId);
            }                
            if (FullnameIsLetters(employee) == false)
            {
                throw new BadRequestException(ValidationProblems.FullNameOnlyLetters);
            }                
            if (await FullnameAndIdIsDistinct(employee) == false)
            {
                throw new BadRequestException(ValidationProblems.FullNameСollision);
            }                
            if (await IsNotCio(employee) == false)
            {
                throw new BadRequestException(ValidationProblems.FullNameСollision);
            }               

            try
            {
                _unitOfWork.Employees.Update(employee);

                await _unitOfWork.CompleteAsync();
            }
            catch (Exception)
            {
                if (await _unitOfWork.Employees.ExistsAsync(g => g.Id == id) == false)
                {
                    throw new NotFoundException();
                }

                throw new Exception();
            }
        }

        // Helpers
        private async Task AttachDepartment(Employee employee)
        {
            employee.Department = await _unitOfWork.Departments.GetAsync(employee.DepartmentId);
        }
        private async Task AttachRoles(Employee employee)
        {
            if (employee.EmployeeRole != null)
            {
                foreach (var er in employee.EmployeeRole)
                {
                    er.Role = await _unitOfWork.Roles.GetAsync(er.RoleId);
                }
            }
        }

        // Validation Helpers
        private bool FullnameIsLetters(Employee employee)
        {
            return employee.FirstName.All(char.IsLetter) && employee.LastName.All(char.IsLetter);
        }
        private async Task<bool> FullnameIsDistinct(Employee employee)
        {
            return await _unitOfWork.Employees
                .ExistsAsync(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName) is false;
        }
        private async Task<bool> FullnameAndIdIsDistinct(Employee employee)
        {
            return await _unitOfWork.Employees
                .ExistsAsync(e => e.Id != employee.Id && 
                e.FirstName == employee.FirstName && 
                e.LastName == employee.LastName)
                is false;
        }
        private async Task<bool> IsNotCio(Employee employee)
        {
            bool output = true;
            if (employee.EmployeeRole == null)
            {
                return output;
            }
            else if (employee.EmployeeRole.Any(e => e.RoleId == (int)RoleEnum.Cio))
            {
                return await _unitOfWork.Employees.ExistsAsync(e => e.EmployeeRole.Any(e => e.RoleId == (int)RoleEnum.Cio));
            }
            return output;
        }
    }
}
