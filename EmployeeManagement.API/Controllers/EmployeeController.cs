using EmployeeManagement.Common.Dtos.Employee;
using EmployeeManagement.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        public IEmployeeService _employeeService { get; }
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreateEmployee(EmployeeCreate employeeCreate)
        {
            var id = await _employeeService.CreateEmployeeAsync(employeeCreate);
            return Ok(id);
        }
        
        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateEmployee(EmployeeUpdate employeeUpdate)
        {
            await _employeeService.UpdateEmployeeAsync(employeeUpdate);
            return Ok();
        }
        
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteEmployee(EmployeeDelete employeeDelete)
        {
            await _employeeService.DeleteEmployeeAsync(employeeDelete);
            return Ok();
        }

        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _employeeService.GetEmployeeAsync(id);
            return Ok(employee);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetEmployees([FromQuery] EmployeeFilter employeeFilter)
        {
            var employees = await _employeeService.GetEmployeesAsync(employeeFilter);
            return Ok(employees);
        }
    }
}
