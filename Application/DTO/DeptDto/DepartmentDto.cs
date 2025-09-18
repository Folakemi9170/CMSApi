namespace CMSApi.Application.DTO.DeptDto
{
    public class DepartmentResponseDto
    {
            public string Message { get; set; }
            public IEnumerable<ResponseDto> Departments { get; set; }
    }
}
