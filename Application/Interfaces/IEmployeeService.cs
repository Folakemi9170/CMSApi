using CMSApi.Application.DTO.EmployeeDto;
using CMSApi.Application.DTO.EmployeeDto.Paging;

namespace CMSApi.Application.Interfaces
{
    public interface IEmployeeService
    {

        Task<EmployeeResponseDto> CreateEmployee(CreateEmployeeDto employee);
        Task<EmployeeResponseDto> UpdateEmployee( int id, UpdateEmployeeDto employee);
        Task<EmployeeResponseDto> GetEmployeesById(int id);
        Task<PagedResponse<EmployeeResponseDto>> GetAllEmployees(Filter filter = null, Pagination pagination = null);
        Task<EmployeeResponseDto> PatchEmployee(int id, PatchEmployeeDto employeedto);
    }
}
