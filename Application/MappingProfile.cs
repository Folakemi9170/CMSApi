using AutoMapper;
//using CMSApi.Application.DTO.AuthDto;
using CMSApi.Application.DTO.DeptDto;
using CMSApi.Application.DTO.EmployeeDto;
using CMSApi.Domain.Entities;

namespace CMSApi.Application
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain → DTO
            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.Fullname,
                           opt => opt.MapFrom(src => $"{src.Firstname} {src.Middlename} {src.Lastname}"));

            CreateMap<Department, ResponseDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.DeptName, opt => opt.MapFrom(src => src.DeptName));
            CreateMap<CreateDeptDto, Department>();
            //CreateMap<RegisterDto, Employee>();
            CreateMap<CreateEmployeeDto, Employee>();
            CreateMap<UpdateEmployeeDto, Employee>();
        }
    }
}
