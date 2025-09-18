using CMSApi.Application.DTO.DeptDto;
using CMSApi.Application.DTO.EmployeeDto;
using CMSApi.Application.Interfaces;
using CMSApi.Application.Services;
using CMSApi.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CMSApi.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptController : ControllerBase
    {
        private readonly IDeptService _deptService;
        public DeptController(IDeptService deptService)
        {
            _deptService = deptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments()
        {
            var departments = await _deptService.GetAllDepartments();

            if (!departments.Departments.Any())
                return NotFound(departments); ;

            return Ok(departments);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment([FromBody] CreateDeptDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.DeptName))
                return BadRequest("Department name is required.");

            var result = await _deptService.CreateDepartment(dto);
            return CreatedAtAction(nameof(GetDepartments), new { name = result.DeptName }, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartment(int id)
        {
            var result = await _deptService.GetDepartment(id);

            if (result.Departments == null || !result.Departments.Any())
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DepartmentResponseDto>> UpdateDepartment(int id, [FromBody] UpdateDept department)
        {
            if (department == null)
                return BadRequest();

            var result = await _deptService.Update(id, department);

            if (result.Departments == null || !result.Departments.Any())
                return NotFound(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<DepartmentResponseDto>> DeleteDepartment(int id) 
        {
            var department = await _deptService.Delete(id);
            if (department.Departments == null || !department.Departments.Any())
                return NotFound(department);

            return Ok(department);
        }
    }
}
