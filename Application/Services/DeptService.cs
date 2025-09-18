using AutoMapper;
using CMSApi.Application.DTO.DeptDto;
using CMSApi.Application.DTO.EmployeeDto;
using CMSApi.Application.Interfaces;
using CMSApi.Domain.Entities;
using CMSApi.Infrastructure;
using CMSApi.Infrastructure.Data;


namespace CMSApi.Application.Services
{
    public class DeptService : IDeptService
    {
        private readonly IGenericRepository<Department> _departmentRepo;
        private readonly CMSDbContext _dbContext;
        private readonly IMapper _mapper;


        public DeptService(IGenericRepository<Department> departmentRepo, CMSDbContext dbContext, IMapper mapper)
        {
            _departmentRepo = departmentRepo;
            _mapper = mapper;
            _dbContext = dbContext;

        }


        public async Task<DepartmentResponseDto> GetAllDepartments()
        {
            var departments = _departmentRepo.GetAll();
            if (departments == null || !departments.Any())
            {
                return new DepartmentResponseDto
                {
                    Message = "No departments found",
                    Departments = new List<ResponseDto>()
                };
            }

            return new DepartmentResponseDto
            {
                Message = "Departments retrieved successfully",
                Departments = _mapper.Map<IEnumerable<ResponseDto>>(departments)
            };
        }

        public async Task<ResponseDto> CreateDepartment(CreateDeptDto dto)
        {
            var department = new Department
            {
                DeptName = dto.DeptName,
                CreatedAt = dto.CreatedAt
            };

            var created = await _dbContext.AddAsync(department);
            await _dbContext.SaveChangesAsync();


            return _mapper.Map<ResponseDto>(created.Entity);
        }

        public async Task<DepartmentResponseDto> GetDepartment(int id)
        {
            var department = _departmentRepo.Get(id);
            if (department == null)
            {
                return new DepartmentResponseDto
                {
                    Message = $"Department with id {id} not found",
                    Departments = new List<ResponseDto>()
                };
            }

            return new DepartmentResponseDto
            {
                Message = "Department retrieved successfully",
                Departments = new List<ResponseDto>
                {
                    _mapper.Map<ResponseDto>(department)
                }
            };
        }

        public async Task<DepartmentResponseDto> Update(int id, UpdateDept dto)
        {
            var department = _departmentRepo.Get(id);
            if (dto == null)
            {
                return new DepartmentResponseDto
                {
                    Message = $"Department with id {id} not found",
                    Departments = new List<ResponseDto>()
                };
            }
            department.DeptName = dto.DeptName;
            department.UpdatedAt = DateTime.UtcNow;

            _departmentRepo.Update(department);

            return new DepartmentResponseDto
            {
                Message = "Department updated successfully",
                Departments = new List<ResponseDto>
                {
                    _mapper.Map<ResponseDto>(department)
                }
            };
        }

        public async Task<DepartmentResponseDto> Delete(int id)
        {
            var department = _departmentRepo.Get(id);

            if (department == null)
            {
                return new DepartmentResponseDto
                { 
                    Message = $"Department with id {id} not found",
                    Departments = new List<ResponseDto>()
                };
            }

            _departmentRepo.Delete(id);

            return new DepartmentResponseDto
            {
                Message = "Department deleted successfully",
                Departments = new List<ResponseDto>
                {
                    _mapper.Map<ResponseDto>(department)
                }
            };
        }

    }
}
