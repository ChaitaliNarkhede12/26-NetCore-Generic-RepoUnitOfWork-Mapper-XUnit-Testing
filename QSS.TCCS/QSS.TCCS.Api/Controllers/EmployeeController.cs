using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QSS.TCCS.Application.Interfaces;
using QSS.TCCS.Application.Models.EmployeeViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QSS.TCCS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }

        [HttpGet("GetAllEmployee")]
        public async Task<IActionResult> GetAllEmployee()
        {
            IEnumerable<EmployeeModel> result = await this._employeeService.GetAllEmployee();
            return Ok(result);
        }

        [HttpGet("GetEmployeeById/{id}")]
        public async Task<IActionResult> GetEmployeeById(int id)
        {
            EmployeeModel result = await this._employeeService.GetEmployeeById(id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("GetEmployeeByIdUsingPredicate/{id}")]
        public async Task<IActionResult> GetEmployeeByIdUsingPredicate(int id)
        {
            IEnumerable<EmployeeModel> result = await _employeeService.GetEmployeeById(x => x.Id == id);
            return Ok(result);
        }

        [HttpGet("GetEmployeeSignleOrDefault/{id}")]
        public async Task<IActionResult> GetEmployeeSignleOrDefault(int id)
        {
            EmployeeModel result = await _employeeService.SingleOrDefaultEmployeeAsync(x => x.Id == id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpGet("GetEmployeeFirstOrDefault/{id}")]
        public async Task<IActionResult> GetEmployeeFirstOrDefault(int id)
        {
            EmployeeModel result = await _employeeService.FirstOrDefaultEmployeeAsync(x => x.Id == id);
            if (result == null)
            {
                return NoContent();
            }
            return Ok(result);
        }

        [HttpPost("AddEmployeeAsync")]
        public async Task<IActionResult> AddEmployeeAsync(EmployeeModel employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }

            EmployeeModel result = await _employeeService.AddEmployeeAsync(employee);
            return Ok(result);
        }

        [HttpPost("AddEmployeeRange")]
        public async Task<IActionResult> AddEmployeeRange(IEnumerable<EmployeeModel> employees)
        {
            if (employees == null)
            {
                return BadRequest();
            }
            int result = await _employeeService.AddEmployeeRange(employees);
            return Ok(result);
        }

        [HttpPost("AddEmployeeRangeAsync")]
        public async Task<IActionResult> AddEmployeeRangeAsync(IEnumerable<EmployeeModel> employees)
        {
            if (employees == null)
            {
                return BadRequest();
            }
            int result = await _employeeService.AddEmployeeRangeAsync(employees);
            return Ok(result);
        }


        [HttpPost("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeModel employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            EmployeeModel result = await _employeeService.UpdateEmployee(employee);
            return Ok(result);
        }

        [HttpPost("UpdateEmployeeRange")]
        public async Task<IActionResult> UpdateEmployeeRange(IEnumerable<EmployeeModel> employees)
        {
            if (employees == null)
            {
                return BadRequest();
            }
            int result = await _employeeService.UpdateEmployeeRange(employees);
            return Ok(result);
        }

        [HttpPost("RemoveEmployee")]
        public async Task<IActionResult> RemoveEmployee(EmployeeModel employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            int result = await _employeeService.RemoveEmployee(employee);
            return Ok(result);
        }

        [HttpPost("RemoveEmployeeById")]
        public async Task<IActionResult> RemoveEmployeeById(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            int result = await _employeeService.RemoveEmployeeById(id);
            return Ok(result);
        }

        [HttpPost("RemoveEmployeeByRange")]
        public async Task<IActionResult> RemoveEmployeeByRange(IEnumerable<EmployeeModel> employees)
        {
            if (employees == null)
            {
                return BadRequest();
            }
            int result = await _employeeService.RemoveEmployeeRange(employees);
            return Ok(result);
        }
    }
}
