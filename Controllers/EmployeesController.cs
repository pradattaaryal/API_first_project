using API_first_project.Models;
using API_first_project.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace API_first_project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IErrorHandlingService<string> _errorHandlingService;

        public EmployeeController(IEmployeeService employeeService, IErrorHandlingService<string> errorHandlingService)
        {
            _employeeService = employeeService;
            _errorHandlingService = errorHandlingService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetAllEmployees()
        {
            try
            {
                var employees = await _employeeService.GetAllEmployeesAsync();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                return StatusCode(500, _errorHandlingService.GetError()); // Internal Server Error
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound(); // 404 Not Found
                }
                return Ok(employee);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                return StatusCode(500, _errorHandlingService.GetError()); // Internal Server Error
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddEmployee(Employee employee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // 400 Bad Request for validation errors
                }

                await _employeeService.AddEmployeeAsync(employee);
                return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                return StatusCode(500, _errorHandlingService.GetError()); // Internal Server Error
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                if (id != employee.Id)
                {
                    return BadRequest("Employee ID mismatch."); // 400 Bad Request
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState); // 400 Bad Request for validation errors
                }

                await _employeeService.UpdateEmployeeAsync(employee);
                return NoContent(); // 204 No Content for successful update
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                return StatusCode(500, _errorHandlingService.GetError()); // Internal Server Error
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            try
            {
                var employee = await _employeeService.GetEmployeeByIdAsync(id);
                if (employee == null)
                {
                    return NotFound(); // 404 Not Found
                }

                await _employeeService.DeleteEmployeeAsync(id);
                return NoContent(); // 204 No Content for successful deletion
            }
            catch (Exception ex)
            {
                _errorHandlingService.SetError(ex.Message);
                return StatusCode(500, _errorHandlingService.GetError()); // Internal Server Error
            }
        }
    }
}
