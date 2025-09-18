using AutoMapper;
using CMSApi.Application.DTO.EmployeeDto;
using CMSApi.Application.DTO.EmployeeDto.Paging;
using CMSApi.Application.Interfaces;
using CMSApi.Domain.Entities;
using CMSApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;


namespace CMSApi.Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IGenericRepository<Employee> _employeeRepo;
        private readonly CMSDbContext _dbContext;
        private readonly IMapper _mapper;


        public EmployeeService(IGenericRepository<Employee> employeeRepo, CMSDbContext dbContext, IMapper mapper)
        {
            _employeeRepo = employeeRepo;
            _mapper = mapper;
            _dbContext = dbContext;
           
        }
 
        //POST
        public async Task<EmployeeResponseDto> CreateEmployee(CreateEmployeeDto dto)
        {
            bool exists = _dbContext.Employees.Any(x => x.Email == dto.Email);

            if (exists)
                throw new Exception("Employee already exists.");


            var employees = _mapper.Map<Employee>(dto);

            //var passwordHasher = new PasswordHasher<Employee>();
            //employee.PasswordHash = passwordHasher.HashPassword(employee, dto.Password);

            await _dbContext.Employees.AddAsync(employees);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<EmployeeResponseDto>(employees);
        }


        //GET(all)
        public async Task<PagedResponse<EmployeeResponseDto>> GetAllEmployees(Filter filter = null, Pagination pagination = null)
        {

            var query = _dbContext.Employees
                .Include(e => e.Department)   // bring in department
                .AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Gender))
                    query = query.Where(e => e.Gender == filter.Gender);

                if (filter.DepartmentId != null)
                    query = query.Where(e => e.DepartmentId == filter.DepartmentId);

                if (filter.IsActive.HasValue)
                    query = query.Where(e => e.IsActive == filter.IsActive.Value);
            }

            var totalRecords = await query.CountAsync();
            var pageNumber = pagination?.PageNumber ?? 1;
            var pageSize = pagination?.PageSize ?? 10;

            var employees = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Fullname = $"{e.Firstname} {e.Middlename} {e.Lastname}",
                    Gender = e.Gender,
                    Email = e.Email,
                    Phone = e.Phone,
                    DOB = e.DOB,
                    Role = e.Role,
                    DepartmentId = e.DepartmentId,
                    DeptName = e.Department.DeptName,
                    IsActive = e.IsActive
                })
                .ToListAsync();

            if (!employees.Any())
                return new PagedResponse<EmployeeResponseDto>(new List<EmployeeResponseDto>(), totalRecords, pageNumber, pageSize);

            return new PagedResponse<EmployeeResponseDto>( employees, totalRecords,pageNumber,pageSize);
        }


        //GEt(id)*************************
        //public async Task<EmployeeResponseDto> GetEmployeesById(int id)
        //{
        //    var employee = _employeeRepo.Get(id);
        //    if (employee == null)
        //        return null;
        //     return _mapper.Map<EmployeeResponseDto>(employee);
        //}


        public async Task<EmployeeResponseDto> GetEmployeesById(int id)
        {
            var employee = await _dbContext.Employees
                .Where(e => e.Id == id)
                .Select(e => new EmployeeResponseDto
                {
                    Id = e.Id,
                    Fullname =$"{e.Firstname} {e.Middlename} {e.Lastname}",
                    Gender = e.Gender,
                    Email = e.Email,
                    Phone = e.Phone,
                    DOB = e.DOB,
                    Role = e.Role,
                    DepartmentId = e.DepartmentId,
                    DeptName = e.Department.DeptName,
                    IsActive = e.IsActive
                })
                .FirstOrDefaultAsync();

            return employee;
        }

        public async Task<EmployeeResponseDto> UpdateEmployee(int Id, UpdateEmployeeDto employeeDto)
        {
            //if (Id != employeeDto.Id)
            //    throw new Exception("ID in URL and body do not match");

            var employee = _employeeRepo.Get(Id);
            if (employee == null)
                return null;

            employee.Lastname = employeeDto.Lastname;
            employee.Email = employeeDto.Email;
            employee.Phone = employeeDto.Phone;
            employee.Role = employeeDto.Role;
            employee.DepartmentId = employeeDto.DepartmentId;
            employee.UpdatedAt = DateTime.UtcNow;

            _employeeRepo.Update(employee);
             await _dbContext.SaveChangesAsync();
            return _mapper.Map<EmployeeResponseDto>(employee);
        }

        //PATCH
        public async Task<EmployeeResponseDto> PatchEmployee(int id, PatchEmployeeDto employeedto)
        {
            var employee = _employeeRepo.Get(id);

            if (employee == null)
                throw new Exception($"Employee with ID {id} not found");

            if (employeedto.IsActive.HasValue)
                employee.IsActive = employeedto.IsActive.Value;

            _employeeRepo.Update(employee);
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<EmployeeResponseDto>(employee);
            
        }
    }
}
