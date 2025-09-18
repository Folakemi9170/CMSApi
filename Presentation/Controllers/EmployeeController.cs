using CMSApi.Application.DTO.EmployeeDto;
using CMSApi.Application.DTO.EmployeeDto.Paging;
using CMSApi.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CMSApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application.DTO.EmployeeDto.EmployeeResponseDto>>> GetAll([FromQuery] Filter filter, [FromQuery] Pagination pagination)
        { 
            var employees = await _employeeService.GetAllEmployees(filter, pagination);
            return Ok(employees);
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeResponseDto>> Register([FromBody] CreateEmployeeDto dto)
        {
            var createdEmployee = await _employeeService.CreateEmployee(dto);

            if (createdEmployee == null)
                return StatusCode(500, "Something went wrong while creating the employee.");

            return CreatedAtAction(nameof(GetEmployeeById), new { id = createdEmployee.Id }, createdEmployee);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Application.DTO.EmployeeDto.EmployeeResponseDto>> GetEmployeeById(int id)
        {
            var employee = await _employeeService.GetEmployeesById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        [HttpPut("{Id}")]
        public async Task<ActionResult<EmployeeResponseDto>> UpdateEmployee(int Id, [FromBody] UpdateEmployeeDto employee)
        {
            try
            {
                var result = await _employeeService.UpdateEmployee(Id, employee);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPatch("{Id}")]
        public async Task<ActionResult<EmployeeResponseDto>> PatchEmployee(int Id, [FromBody] PatchEmployeeDto employeedto)
        {
            try
            {
                var result = await _employeeService.PatchEmployee(Id, employeedto);

                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
