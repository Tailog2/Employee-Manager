using EmployeeManager.Domain.Core.Dtos;
using EmployeeManager.Filters;
using EmployeeManager.Services.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IStringLocalizer<EmployeeController> _stringLocalizer;

        public EmployeeController(IEmployeeService employeeService, IStringLocalizer<EmployeeController> stringLocalizer)
        {
            _employeeService = employeeService;
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        [CookieCultureFilter]
        [Route("Roles")]
        public ActionResult<IEnumerable<string>> GetAllRolesAsync()
        {
            Console.WriteLine(Thread.CurrentThread.CurrentCulture.EnglishName);
            Console.WriteLine(Thread.CurrentThread.CurrentUICulture.EnglishName);
            var roles = _stringLocalizer.GetAllStrings();

            return Ok(roles.Select(r => r.Value));
        }

        // GET: api/<EmployeeController>
        /// <summary>
        /// Returns a list of employees and filtering by first name and last name
        /// If query is not specified return all employees
        /// </summary>
        /// <param name="query"></param>
        /// <returns>List of employees</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Employee/
        ///
        /// </remarks>
        /// <response code="200">Returns the list of employees</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetAllAsync(string? query = null)
        {
            var result = new ActionResult<IEnumerable<EmployeeDto>>(await _employeeService.GetEmployeesAsync(query));
            return result;
        }

        // GET api/<EmployeeController>/5
        /// <summary>
        /// Returns an employee by the specified id 
        /// </summary>
        /// <param name="id"></param>
        /// /// <returns>An employee</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /Employee/1
        ///
        /// </remarks>
        /// <response code="200">Returns an employee</response>
        /// <response code="404">If the employee is not found</response>
        [HttpGet("{id}")]
        public async Task<ActionResult<EmployeeDto>> GetAsync(int id)
        {
            var result = new ActionResult<EmployeeDto>(await _employeeService.GetEmployeeAsync(id));
            return result;
        }

        // POST api/<EmployeeController>
        /// <summary>
        /// Create an employee
        /// </summary>
        /// <param name="newEmployeeDto"></param>
        /// <returns>A newly created Employee</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Employee
        ///     {
        ///        "id": 1,
        ///        "firstName": "John",
        ///        "lastName": "Smith",
        ///        "DepartmentId": 1,
        ///        RolesIds: [ 1, 2 ]
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Returns the newly created item</response>
        /// <response code="400">If the employee is invalid</response>
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> PostAsync(NewEmployeeDto newEmployeeDto)
        {
            var employeeDto = await _employeeService.CreateEmployeeAsync(newEmployeeDto);
            var result = CreatedAtAction(nameof(GetAsync), new { id = employeeDto.Id }, employeeDto);
            return result;
        }

        // PUT api/<EmployeeController>/5
        /// <summary>
        /// Update an employee by the specified id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editEmployeeDto"></param>   
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Employee/1
        ///     {
        ///        "id": 1,
        ///        "firstName": "John",
        ///        "lastName": "Smith",
        ///        "DepartmentId": 1,
        ///        RolesIds: [ 1, 2 ]
        ///     }
        ///
        /// </remarks>
        /// <response code="204">No content</response>
        /// <response code="400">If the employee is invalid</response>
        /// <response code="404">If the employee is not found</response>
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, EditEmployeeDto editEmployeeDto)
        {
            await _employeeService.UpdateEmployeeAsync(id, editEmployeeDto);
            return NoContent();
        }

        // DELETE api/<EmployeeController>/5
        /// <summary>
        /// Deletes an employee by the specified id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>No content</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /Employee/1
        ///
        /// </remarks>
        /// <response code="201">Returns the employee was deleted</response>
        /// <response code="404">If the employee is not found</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id);
            return NoContent();
        }
    }
}
